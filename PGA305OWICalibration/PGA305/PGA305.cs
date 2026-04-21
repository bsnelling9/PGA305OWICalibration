using PGA305OWICalibration.Instruments;
using System.Diagnostics;
using TI.eLAB.EVM;

namespace PGA305OWICalibration.PGA305
{
    public class PGA305Device
    {
        private USB2AnyDevice _u2a;
        //NOTE: Move these to common folder and make public later
        //will need to clean them up
        // OWI timing & constants in microseconds
        private const ushort TIME_SETUP = 100;
        private const ushort TIME_LOW = 5000;
        private const ushort TIME_HIGH = 5000;
        private const ushort TIME_STORE = 100;
        private const int FLAGS = 0;
        private const ushort OW_MODE = 5;

        private const ushort ACT_TIME_LOW = 100;
        private const ushort ACT_TIME_HIGH = 100;

        /*
        GPIO7 is used for OWI activation pulse (GPIO_OWI_ACT)
        GPIO10 is used for OWI VDD control (GPIO_OWI_VDD)
        GPIO11 is used for OWI TX control (GPIO_OWI_TX)
        */
       
        public const byte GPIO4 = 4;
        public const byte GPIO5 = 5;
        public const byte GPIO7 = 7;
        public const byte GPIO10 = 10;
        public const byte GPIO11 = 11;

        // GPIO pin configuration
        public const byte FN_OUTPUT = 1;
        public const byte FN_INPUT = 0;
        public const byte STATE_HIGH = 2;
        public const byte STATE_LOW = 1;

        private const DACs_WhichDAC DAC_OWI_OFFSET = DACs_WhichDAC.DAC0;
        private const byte DAC_OFFSET_ZERO = 0;

        // PGA305 OWI commands
        private const byte SYNC_BYTE = 0x55;
        private const byte CMD_WRITE_PAGE0 = 0x01;
        private const byte CMD_WRITE = 0x51;
        private const byte CMD_READ_INIT = 0x52;
        private const byte CMD_READ_RESPONSE = 0x73;

        // EEPROM addresses
        public const byte ADDR_PN_LSB = 0x70;
        public const byte ADDR_PN_MID = 0x71;
        public const byte ADDR_PN_MSB = 0x72;

        public const byte ADDR_SERIAL_LSB = 0x73;
        public const byte ADDR_SERIAL_MID = 0x74;
        public const byte ADDR_SERIAL_MSB = 0x75;
        
        // EEPROM cache address
        // Part number
        public const byte CACHE_00 = 0x80;
        public const byte CACHE_01 = 0x81;
        public const byte CACHE_02 = 0x82;
        //Serial number
        public const byte CACHE_03 = 0x83;
        public const byte CACHE_04 = 0x84;
        public const byte CACHE_05 = 0x85;

        //Pressure range
        public const byte CACHE_06 = 0x86;
        public const byte CACHE_07 = 0x87;

        // OWI response frame structure (confirmed from Saleae captures)
        // [echo] [0xF6] [DATA] [0xFF] [0xF7]
        // Data is always at index 2
        // Stop sequence is always 0xFF 0xF7
        private const byte OWI_STOP_BYTE = 0xF7;
        private const byte OWI_PRE_STOP_BYTE = 0xFF;
        private const int OWI_DATA_INDEX = 2;
        private const int OWI_MIN_RESPONSE_LEN = 5;
        private const int OWI_READ_TIMEOUT_MS = 200;


        public PGA305Device(USB2AnyDevice device) => _u2a = device;

        public  bool Initialize()
        {
            Debug.WriteLine("Initialise() called");
            _u2a.EnableDebugLogging();

            _u2a.Power_WriteControl(Power_3V3.ON, Power_5V0.ON);
            Thread.Sleep(50);

            int result = _u2a.OneWire_SetMode(OW_MODE);

            Debug.WriteLine($"OneWire_SetMode({OW_MODE}) result: {result}");
            if (result < 0) return false;

            result = _u2a.OneWire_PulseSetup(TIME_SETUP, TIME_LOW, TIME_HIGH, TIME_STORE, FLAGS);
            Debug.WriteLine($"OneWire_PulseSetup result: {result}");
            if (result < 0) return false;

            _u2a.OneWire_SetOutput(0);

            result = _u2a.UART_Control();
            Debug.WriteLine($"UART_Control result: {result}");
            if (result < 0) return false;

            // Per PGA305 GUI documentation: receiver mode 2 = half duplex
            /*result = _u2a.UART_SetMode(2);
            System.Diagnostics.Debug.WriteLine($"UART_SetMode(2) result: {result}");
            if (result < 0) return false;*/

            // Per OWI documentation: 25ms timeout for >4800 bps
            _u2a.SetReceiveTimeout(25);

            Thread.Sleep(20);

            _u2a.GPIO_SetPort(GPIO7, FN_OUTPUT);
            _u2a.GPIO_WritePort(GPIO7, STATE_LOW);

            _u2a.GPIO_SetPort(GPIO11, FN_OUTPUT);
            _u2a.GPIO_WritePort(GPIO11, STATE_LOW);

            _u2a.GPIO_SetPort(GPIO10, FN_OUTPUT);
            _u2a.GPIO_WritePort(GPIO10, STATE_LOW);
                       
            _u2a.DACs_Write(DAC_OWI_OFFSET, DACs_OperatingMode.Normal, DAC_OFFSET_ZERO);
            //Thread.Sleep(10);

            return true;
        }

        public  bool Activate()
        {
            Debug.WriteLine("Activate() called");

            // Note: this sets GPIO4 on U2A to High to setup the pulse time
            _u2a.OneWire_PulseSetup(TIME_SETUP, ACT_TIME_LOW, ACT_TIME_HIGH, TIME_STORE, FLAGS);

            int result = _u2a.OneWire_PulseWriteEx(0, 2);
            Debug.WriteLine($"OneWire_PulseWriteEx result: {result}");

            Thread.Sleep(20);

            result = _u2a.OneWire_PulseWriteEx(0, 2);
            Debug.WriteLine($"OneWire_PulseWriteEx result 2: {result}");

            byte[] unlockReg8 = new byte[] { 0x55, CMD_WRITE_PAGE0, 0x08, 0x55 };
            byte[] unlockReg9 = new byte[] { 0x55, CMD_WRITE_PAGE0, 0x09, 0x55 };

            _u2a.OneWire_PulseSetup(TIME_SETUP, TIME_LOW, TIME_HIGH, TIME_STORE, FLAGS);

            _u2a.UART_Control();
            
            _u2a.GPIO_WritePort(GPIO11, STATE_HIGH);
            _u2a.UART_Write(unlockReg8, (byte)unlockReg8.Length);

            _u2a.UART_Write(unlockReg9, (byte)unlockReg9.Length);
            
            Debug.WriteLine("Activate complete - 3 Pulses sent and Interface Unlocked");

            //_u2a.UART_SetMode(2);
            //_u2a.GPIO_WritePort(GPIO10, STATE_LOW);

            return true;
        }

        public void SetPins()
        {
            _u2a.GPIO_WritePort(GPIO11, STATE_HIGH);
    
            _u2a.GPIO_WritePort(GPIO10, STATE_HIGH);              
        }
       
            
        public int ReadRegister(byte registerAddress)
        {
            byte[] flush = new byte[54];
            byte[] response = new byte[54];

            _u2a.UART_Write(new byte[] { SYNC_BYTE, CMD_READ_INIT, registerAddress, SYNC_BYTE, CMD_READ_RESPONSE }, 5);

            int responseCount = _u2a.UART_Read(response, 54);
                   

            Debug.WriteLine($"Response count: {responseCount}");
            for (int i = 0; i < responseCount; i++)
                Debug.WriteLine($"  response[{i}] = 0x{response[i]:X2}");

            return response[0];
        }


        public byte[] ReadEepromCache()
        {
            byte[] flush = new byte[54];
            byte[] data = new byte[54];

            _u2a.UART_Write(new byte[] { SYNC_BYTE, CMD_READ_INIT, 0x70 }, 3);
            _u2a.UART_Read(flush, 54);

            _u2a.UART_Write(new byte[] { SYNC_BYTE, CMD_READ_RESPONSE }, 2);
            _u2a.UART_Read(data, 54);

            for (int i = 0; i < 8; i++)
                Debug.WriteLine($"  cache[{i}] = 0x{data[i]:X2}");

            return data;
        }

        public (string partNumber, string serialNumber, int prange) ReadMetadata()
        {
            byte[] data = new byte[54];

            _u2a.UART_Write(new byte[] { SYNC_BYTE, CMD_READ_INIT, 0x70 }, 3);
            _u2a.UART_Write(new byte[] { SYNC_BYTE, 0xD3 }, 2);
            int count = _u2a.UART_Read(data, 54);

            Debug.WriteLine($"Cache read count: {count}");
            for (int i = 0; i < count; i++)
                Debug.WriteLine($"  cache[{i}] = 0x{data[i]:X2}");

            // Parse part number
            int pn_lsb = data[0];
            int pn_mid = data[1];
            int pn_msb = data[2];
            string partNumber = $"0x{pn_msb:X2}{pn_mid:X2}{pn_lsb:X2}";


            // Parse serial number
            long serialValue = ((long)data[5] << 16) | ((long)data[4] << 8) | (long)data[3];
            string serialNumber = serialValue.ToString("D6");

            // Parse pressure range
            int prange = data[6] | (data[7] << 8);

            return (partNumber, serialNumber, prange);
        }

        public string ReadPartNumber()
        {
            byte[] cache = ReadEepromCache();

            int lsb = cache[0];
            int mid = cache[1];
            int msb = cache[2];

            long partNumberValue = ((long)msb << 16) | ((long)mid << 8) | (long)lsb;
            Debug.WriteLine($"Part number: msb:0x{msb:X2} mid:0x{mid:X2} lsb:0x{lsb:X2}");
            Debug.WriteLine($"Part number value: {partNumberValue} (0x{partNumberValue:X6})");

            return $"0x{msb:X2}{mid:X2}{lsb:X2}";
        }

        public string ReadSerialNumber()
        {
            byte[] cache = ReadEepromCache();

            int b0 = cache[3];
            int b1 = cache[4];
            int b2 = cache[5];

            long serialValue = ((long)b2 << 16) | ((long)b1 << 8) | (long)b0;
            Debug.WriteLine($"Serial: b2:0x{b2:X2} b1:0x{b1:X2} b0:0x{b0:X2}");

            return serialValue.ToString("D6");
        }
                       
        public void LoadEepromCache(byte page)
        {
            byte[] buffer = new byte[54];
                        
            _u2a.UART_Write(new byte[] { SYNC_BYTE, 0x21, 0x88, page }, 4);
            _u2a.UART_Write(new byte[] { SYNC_BYTE, 0x21, 0x89, 0x01 }, 4);

            Thread.Sleep(20);
            //_u2a.GPIO_WritePort(GPIO11, STATE_LOW);

            //_u2a.UART_Read(buffer, 54);

            Debug.WriteLine($"EEPROM cache loaded for page {page}");
        }
    }
}

/* public string ReadPartNumber()
{
    //int lsb = ReadRegister(ADDR_PN_LSB);
    //int mid = ReadRegister(ADDR_PN_MID);
    //int msb = ReadRegister(ADDR_PN_MSB);

    int lsb = ReadRegister(CACHE_00);
    int mid = ReadRegister(CACHE_01);
    int msb = ReadRegister(CACHE_02);

    Debug.WriteLine($"Part number: msb: {msb} | mid: {mid} | lsb: {lsb}");

    Debug.WriteLine($"Part number: msb:0x{msb:X2} mid:0x{mid:X2} lsb:0x{lsb:X2}");

    long partNumberValue = ((long)msb << 16) | ((long)mid << 8) | (long)lsb;
    Debug.WriteLine($"Part number value: {partNumberValue} (0x{partNumberValue:X6})");

    if (lsb < 0 || mid < 0 || msb < 0) return "Read error";

    return $"0x{msb:X2}{mid:X2}{lsb:X2}";
}
*/

       /* public string ReadSerialNumber()
        {
            int b0 = ReadRegister(ADDR_SERIAL_LSB);
            int b1 = ReadRegister(ADDR_SERIAL_MID);
            int b2 = ReadRegister(ADDR_SERIAL_MSB);
            
            Debug.WriteLine($"Serial Number: b2: {b2} | b1: {b1} | b0: {b0}");

            Debug.WriteLine($"Serial Number: b2:0x{b2:X2} b1:0x{b1:X2} b0:0x{b0:X2}");
                      
            if (b0 < 0 || b1 < 0 || b2 < 0  )
                return "Read error";
                        
            long serialValue = ((long)b2 << 16) | ((long)b1 << 8) | (long)b0;
            Debug.WriteLine($"{serialValue}");

            return serialValue.ToString("D6");
        }*/


/*
 * Other code for the read resgister
 * _u2a.UART_Write(new byte[] { SYNC_BYTE, CMD_READ_INIT, registerAddress }, 3);
        Thread.Sleep(50);  // wait for TX to complete and PGA305 to respond
        int flushCount = _u2a.UART_Read(flush, 54);
        Debug.WriteLine($"Flush count: {flushCount}");
        for (int i = 0; i < flushCount; i++)
            Debug.WriteLine($"  flush[{i}] = 0x{flush[i]:X2}");

        _u2a.UART_Write(new byte[] { SYNC_BYTE, CMD_READ_RESPONSE }, 2);
        Thread.Sleep(50);  // wait for TX to complete and PGA305 to respond
        int responseCount = _u2a.UART_Read(response, 54);*/