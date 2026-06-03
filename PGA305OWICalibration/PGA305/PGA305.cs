using PGA305OWICalibration.Instruments;
using System.Diagnostics;

namespace PGA305OWICalibration.PGA305
{
    public class PGA305Device
    {
        private USB2AnyDevice _u2a;
        //NOTE: Move these to common folder and make public later
        //will need to clean them up
        // OWI timing & constants in microseconds
        private const ushort TIME_SETUP = 1000;
        private const ushort TIME_LOW = 5000;
        private const ushort TIME_HIGH = 5000;
        private const ushort TIME_STORE = 1000;
        private const int FLAGS = 0;
        private const ushort OW_MODE = 5;

        private const ushort ACT_TIME_LOW = 1000;
        private const ushort ACT_TIME_HIGH = 1000;

        /*
        GPIO5 is used for OWI Rx
        GPIO4 is used for OWI Tx
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
        public const byte PIN_OUTPUT = 1;
        public const byte PIN_INPUT = 2;
        public const byte PIN_INPUT_PULLUP = 3;
        public const byte STATE_HIGH = 2;
        public const byte STATE_LOW = 1;


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
        
        // EEPROM cache addresses
        public const byte CACHE_00 = 0x80;
        public const byte CACHE_01 = 0x81;
        public const byte CACHE_02 = 0x82;
        public const byte CACHE_03 = 0x83;
        public const byte CACHE_04 = 0x84;
        public const byte CACHE_05 = 0x85;
        public const byte CACHE_06 = 0x86;
        public const byte CACHE_07 = 0x87;


        public PGA305Device(USB2AnyDevice device) => _u2a = device;

        public bool Initialize()
        {
            Debug.WriteLine("Initialise() device called");
            _u2a.EnableDebugLogging();

            int result = _u2a.OneWire_SetMode(OW_MODE);

            Debug.WriteLine($"OneWire_SetMode({OW_MODE}) result: {result}");
            if (result < 0) return false;

            _u2a.OneWire_SetOutput(0);

            _u2a.UART_Control();
            _u2a.UART_SetMode(2);

            _u2a.GPIO_SetPort(GPIO7, PIN_OUTPUT);
            _u2a.GPIO_WritePort(GPIO7, STATE_LOW);

            _u2a.GPIO_SetPort(GPIO11, PIN_OUTPUT);
            _u2a.GPIO_WritePort(GPIO11, STATE_LOW);

            return true;
        }

        public  bool Activate()
        {
            Debug.WriteLine("Activate called");
            byte[] response = new byte[54];
            byte[] drain = new byte[54];

            _u2a.OneWire_PulseSetup(TIME_SETUP, ACT_TIME_LOW, ACT_TIME_HIGH, TIME_STORE, FLAGS);

            _u2a.OneWire_PulseWriteEx(1, 2);
            Thread.Sleep(55);
            _u2a.OneWire_PulseWriteEx(1, 2);

            _u2a.GPIO_WritePort(GPIO11, STATE_HIGH);
            
            _u2a.UART_Write(new byte[] {
                 SYNC_BYTE, CMD_WRITE_PAGE0, 0x08, SYNC_BYTE,
                 SYNC_BYTE, CMD_WRITE_PAGE0, 0x09, SYNC_BYTE
             }, 8);

             _u2a.UART_Write(new byte[] { SYNC_BYTE, 0x02, 0x0C, SYNC_BYTE, CMD_READ_RESPONSE }, 5);
             int count = _u2a.UART_Read(response, 54);

             Debug.WriteLine($"Poll : got {count} bytes");
             for (int i = 0; i < count; i++)
             {
                 Debug.WriteLine($"  [{i}] = 0x{response[i]:X2}");
             }

             byte counter = 0;
             bool commandModeActive = false;

             while (counter < 5)
             {
                 _u2a.UART_Write(new byte[] { SYNC_BYTE, 0x02, 0x0C, SYNC_BYTE, CMD_READ_RESPONSE }, 5);
                 _u2a.UART_Read(response, 54);

                 Debug.WriteLine($"Poll {counter}: {response}");

                 byte result = response[0];
                 Debug.WriteLine($"Poll {counter}: COMPENSATION_CONTROL = 0x{result:X2}");

                 if (result == 0x03)
                 {
                     commandModeActive = true;
                     Debug.WriteLine("Command mode confirmed.");
                     break;
                 }

                 counter++;
             }

             if (!commandModeActive)
             {
                 Debug.WriteLine("Error: Failed to establish OWI command mode after 255 attempts.");
                 return false;
             }

             Debug.WriteLine("Activate complete d");

            return true;
        }


        public int ReadRegister(byte registerAddress)
        {
            byte[] flush = new byte[54];
            byte[] response = new byte[54];

            _u2a.UART_Write(new byte[] { SYNC_BYTE, CMD_READ_INIT, registerAddress, SYNC_BYTE, CMD_READ_RESPONSE }, 5);

            int count = _u2a.UART_Read(response, 54);

            Debug.WriteLine($"Poll : got {count} bytes");
            for (int i = 0; i < count; i++)
            {
                Debug.WriteLine($"  [{i}] = 0x{response[i]:X2}");
            }

            Debug.WriteLine($"Reg 0x{registerAddress:X2} count Raw: [0x{response[0]:X2}]");

            return response[0];
        }

        public string ReadPartNumber()
        {
            int lsb = ReadRegister(ADDR_PN_LSB);
            int mid = ReadRegister(ADDR_PN_MID);
            int msb = ReadRegister(ADDR_PN_MSB);

            Debug.WriteLine($"Part number: msb: {msb} | mid: {mid} | lsb: {lsb}");

            Debug.WriteLine($"Part number: msb:0x{msb:X2} mid:0x{mid:X2} lsb:0x{lsb:X2}");

            if (lsb < 0 || mid < 0 || msb < 0) return "Read error";

            string prefix = (msb % 128) == 0 ? "A" : "S";
            int numeric = lsb + (mid << 8) + ((msb / 128) << 16);
            string partNumber = prefix + numeric.ToString();

            Debug.WriteLine($"Part Number: {partNumber}");

            return partNumber;
        }

        public string ReadSerialNumber()
        {
            int lsb = ReadRegister(ADDR_SERIAL_LSB);
            int mid = ReadRegister(ADDR_SERIAL_MID);
            int msb = ReadRegister(ADDR_SERIAL_MSB);

            Debug.WriteLine($"Part number: msb: {msb} | mid: {mid} | lsb: {lsb}");

            Debug.WriteLine($"Part number: msb:0x{msb:X2} mid:0x{mid:X2} lsb:0x{lsb:X2}");

            if (lsb < 0 || mid < 0 || msb < 0) return "Read error";

            int serialValue = lsb + (mid << 8) + (msb << 16);
            string serialNumber = serialValue.ToString("D6");

            Debug.WriteLine($"Serial Number: {serialNumber}");
            
            return serialNumber;
        }

        public string ReadDIG_IF_CNTRL()
        {
            int value = ReadRegister(0x0B);
            if (value < 0) return "Read error";
            return $"DIG_IF_CNTRL: 0x{value:X2}";
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

            int pn_lsb = data[0];
            int pn_mid = data[1];
            int pn_msb = data[2];
            string partNumber = $"0x{pn_msb:X2}{pn_mid:X2}{pn_lsb:X2}";

            long serialValue = ((long)data[5] << 16) | ((long)data[4] << 8) | (long)data[3];
            string serialNumber = serialValue.ToString("D6");

            int prange = data[6] | (data[7] << 8);

            return (partNumber, serialNumber, prange);
        }
             
       //remove this
        public void LoadEepromCache(byte page)
        {
            byte[] buffer = new byte[54];
                        
            _u2a.UART_Write(new byte[] { SYNC_BYTE, 0x21, 0x88, page }, 4);
            _u2a.UART_Write(new byte[] { SYNC_BYTE, 0x21, 0x89, 0x01 }, 4);

            Thread.Sleep(20);

            Debug.WriteLine($"EEPROM cache loaded for page {page}");
        }
    }
}
