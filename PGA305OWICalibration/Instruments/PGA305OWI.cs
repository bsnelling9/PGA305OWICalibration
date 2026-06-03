using PGA305OWICalibration.Instruments;
using System.Diagnostics;

namespace PGA305OWICalibration.PGA305EVM
{
    public class PGA305Owi
    {
        private USB2AnyDevice _u2a;
        //NOTE: Move these to common folder and make public later
        // OWI timing & constants in microseconds
        private const ushort TIME_SETUP = 1000;
        private const ushort TIME_STORE = 1000;
        private const int FLAGS = 0;
        private const ushort OW_MODE = 5;

        private const ushort ACT_TIME_LOW = 1000;
        private const ushort ACT_TIME_HIGH = 1000;

        /*
        GPIO7 is used for OWI activation pulse (GPIO_OWI_ACT)
        GPIO10 is used for OWI VDD control (GPIO_OWI_VDD)
        GPIO11 is used for OWI TX control (GPIO_OWI_TX)
        */
        public const byte GPIO0 = 0;
        public const byte GPIO1 = 1;
        public const byte GPIO4 = 4;
        public const byte GPIO5 = 5;
        public const byte GPIO7 = 7;
        public const byte GPIO10 = 10;
        public const byte GPIO11 = 11;

        // GPIO pin configuration
        public const byte FN_OUTPUT = 1;
        public const byte FN_INPUT = 2;
        public const byte FN_INPUT_PULLUP = 3;
        public const byte STATE_HIGH = 2;
        public const byte STATE_LOW = 1;


        // PGA305 OWI commands
        private const byte SYNC_BYTE = 0x55;
        private const byte CMD_WRITE = 0x51;
        private const byte CMD_READ_INIT = 0x52;
        private const byte CMD_READ_RESPONSE = 0x73;
        private const byte CMD_WRITE_PAGE0 = 0x01;

        // EEPROM addresses
        public const byte ADDR_PN_LSB = 0x70;
        public const byte ADDR_PN_MID = 0x71;
        public const byte ADDR_PN_MSB = 0x72;

        public const byte ADDR_SERIAL_LSB = 0x73;
        public const byte ADDR_SERIAL_MID = 0x74;
        public const byte ADDR_SERIAL_MSB = 0x75;

        //Interanal Serial Number EEPROM Addresses
        public const byte INTERNAL_SN_b0 = 0x64;
        public const byte INTERNAL_SN_b1 = 0x65;
        public const byte INTERNAL_SN_b2 = 0x66;
        public const byte INTERNAL_SN_b3 = 0x67;

        public PGA305Owi(USB2AnyDevice device) => _u2a = device;
      
        public bool Initialize()
        {
            Debug.WriteLine("Initialise() called");

            int result = _u2a.OneWire_SetMode(OW_MODE);
            Debug.WriteLine($"OneWire_SetMode({OW_MODE}) result: {result}");
            if (result < 0) return false;

            _u2a.OneWire_SetOutput(0);
            _u2a.SetReceiveTimeout(25);

            int uartResult = _u2a.UART_Control();
            Debug.WriteLine($"UART_Control result: {uartResult}");

            _u2a.GPIO_SetPort(GPIO10, FN_OUTPUT);
            _u2a.GPIO_WritePort(GPIO10, STATE_LOW);

            _u2a.GPIO_SetPort(GPIO11, FN_OUTPUT);
            _u2a.GPIO_WritePort(GPIO11, STATE_LOW);
            
            return true;
        }

        public bool Activate()
        {
            Debug.WriteLine("Activate called OWI");
            byte[] response = new byte[54];

            _u2a.OneWire_PulseSetup(TIME_SETUP, ACT_TIME_LOW, ACT_TIME_HIGH, TIME_STORE, FLAGS);

            _u2a.OneWire_PulseWriteEx(0, 2);
            Thread.Sleep(20);
            _u2a.OneWire_PulseWriteEx(0, 2);

            _u2a.GPIO_WritePort(GPIO11, STATE_HIGH);

            _u2a.UART_Write(new byte[] {
                SYNC_BYTE, CMD_WRITE_PAGE0, 0x08, SYNC_BYTE,
                SYNC_BYTE, CMD_WRITE_PAGE0, 0x09, SYNC_BYTE
            }, 8);

            _u2a.UART_Write(new byte[] { SYNC_BYTE, 0x02, 0x0C, SYNC_BYTE, CMD_READ_RESPONSE }, 5);
            int count = _u2a.UART_Read(response, 54);

            Debug.WriteLine($"Activate: got {count} bytes");
            for (int i = 0; i < count; i++)
                Debug.WriteLine($"  [{i}] = 0x{response[i]:X2}");


            if (count > 0 && response[count - 1] == 0x03)
            {
                Debug.WriteLine("Command mode confirmed.");
                return true;
            }

            Debug.WriteLine("Error: Failed to establish OWI command mode.");
            return false;
        }

        public void LoadEepromCache(byte page)
        {
            byte[] buffer = new byte[54];
            _u2a.UART_Write(new byte[] { SYNC_BYTE, CMD_WRITE, 0x88, page }, 4);
            _u2a.UART_Write(new byte[] { SYNC_BYTE, CMD_WRITE, 0x89, 0x01 }, 4);
            _u2a.UART_Read(buffer, 54);
            Debug.WriteLine($"EEPROM cache loaded for page {page}");
        }

        public async Task<int> ReadRegisterAsync(byte registerAddress)
        {
            return await Task.Run(() =>
            {
                byte[] flush = new byte[54];
                byte[] return_data = new byte[54];

                _u2a.UART_Read(flush, 54);

                _u2a.UART_Write(new byte[] {
                    SYNC_BYTE, CMD_READ_INIT, registerAddress,
                    SYNC_BYTE, CMD_READ_RESPONSE
                }, 5);

                            var sw = System.Diagnostics.Stopwatch.StartNew();
                while (_u2a.UART_GetRxCount() == 0 && sw.ElapsedMilliseconds < 100) { }

                int count = _u2a.UART_Read(return_data, 54);

                Debug.WriteLine($"Reg 0x{registerAddress:X2} count:{count} Raw: [{string.Join(" ", return_data.Take(10).Select(b => $"0x{b:X2}"))}]");

                if (count < 5) return -1;
                return (int)return_data[3];
            });
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

            if (lsb< 0 || mid < 0 || msb < 0) return "Read error";
            
            int serialValue = lsb + (mid << 8) + (msb << 16);
            string serialNumber = serialValue.ToString("D6");

            Debug.WriteLine($"Serial Number: {serialNumber}");
            return serialNumber;
        }

        public string ReadInternalSerialNumber()
        {
            int b4 = ReadRegister(INTERNAL_SN_b0);
            int b5 = ReadRegister(INTERNAL_SN_b1);
            int b6 = ReadRegister(INTERNAL_SN_b2);
            int b7 = ReadRegister(INTERNAL_SN_b3);

            Debug.WriteLine($"Sensor Serial: b7:0x{b7:X2} b6:0x{b6:X2} b5:0x{b5:X2} b4:0x{b4:X2}");

            if (b4 < 0 || b5 < 0 || b6 < 0 || b7 < 0)
                return "Read error";

            long serialValue = ((long)b7 << 24) | ((long)b6 << 16) | ((long)b5 << 8) | (long)b4;
            Debug.WriteLine($"Sensor Serial Number: {serialValue}");

            return serialValue.ToString("D10");
        }

        public async Task<string> ReadPartNumberAsync()
        {
            int lsb = await ReadRegisterAsync(ADDR_PN_LSB);

            int mid = await ReadRegisterAsync(ADDR_PN_MID);

            int msb = await ReadRegisterAsync(ADDR_PN_MSB);

            Debug.WriteLine($"Part number: msb: {msb} | mid: {mid} | lsb: {lsb}");
            Debug.WriteLine($"Part number: {msb:X2}{mid:X2}{lsb:X2}");

            return (lsb < 0 || mid < 0 || msb < 0) ? "Read error"
            : $"0x{msb:X2}{mid:X2}{lsb:X2}";
        }
    }
}