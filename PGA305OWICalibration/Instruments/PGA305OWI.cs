using PGA305OWICalibration.Config;
using PGA305OWICalibration.Instruments;
using System.Diagnostics;

namespace PGA305OWICalibration.PGA305EVM
{
    public class PGA305Owi
    {
        private USB2AnyDevice _u2a;

        // OWI timing constants in microseconds — OWI-protocol specific, stay local
        private const ushort TIME_SETUP = 1000;
        private const ushort TIME_STORE = 1000;
        private const int FLAGS = 0;
        private const ushort OW_MODE = 5;
        private const ushort ACT_TIME_LOW = 1000;
        private const ushort ACT_TIME_HIGH = 1000;

        // GPIO pin function/state codes — protocol-level, stay local
        public const byte FN_OUTPUT = 1;
        public const byte FN_INPUT = 2;
        public const byte FN_INPUT_PULLUP = 3;
        public const byte STATE_HIGH = 2;
        public const byte STATE_LOW = 1;

        // PGA305 OWI command bytes — protocol-level, stay local
        private const byte SYNC_BYTE = 0x55;
        private const byte CMD_WRITE = 0x51;
        private const byte CMD_READ_INIT = 0x52;
        private const byte CMD_READ_RESPONSE = 0x73;
        private const byte CMD_WRITE_PAGE0 = 0x01;

        public const byte GPIO7 = 7;
        public const byte GPIO10 = 10;
        public const byte GPIO11 = 11;


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
                FlushUartRx();
                return true;
            }

            Debug.WriteLine("Error: Failed to establish OWI command mode.");
            return false;
        }

        private void FlushUartRx()
        {
            byte[] discard = new byte[54];
            int leftover = _u2a.UART_Read(discard, 54);
            if (leftover > 0)
                Debug.WriteLine($"Flushed {leftover} stale byte(s) from UART RX.");
        }

        public int ReadRegister(byte registerAddress)
        {
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

        public string ReadPressureCode()
        {
            int lsb = ReadRegister(AppConfig.PRANGE_LSB);
            int msb = ReadRegister(AppConfig.PRANGE_MSB);
            int accuracyByte = ReadRegister(AppConfig.ACCURACY);

            Debug.WriteLine($"Pressure code: lsb:0x{lsb:X2} msb:0x{msb:X2} accuracy:0x{accuracyByte:X2}");

            if (lsb < 0 || msb < 0 || accuracyByte < 0) return "Read error";

            int pressureValue = lsb | (msb << 8);
            char accuracy = (char)accuracyByte;

            string pressureCode = $"{pressureValue:D3}{accuracy}";

            Debug.WriteLine($"Pressure Code: {pressureCode}");

            return pressureCode;
        }

        public string ReadSerialNumber()
        {
            int lsb = ReadRegister(AppConfig.SENSOR_SN_B0);
            int mid = ReadRegister(AppConfig.SENSOR_SN_B1);
            int msb = ReadRegister(AppConfig.SENSOR_SN_B2);

            Debug.WriteLine($"Sensor serial: msb:0x{msb:X2} mid:0x{mid:X2} lsb:0x{lsb:X2}");

            if (lsb < 0 || mid < 0 || msb < 0) return "Read error";

            int serialValue = lsb + (mid << 8) + (msb << 16);
            string serialNumber = serialValue.ToString("D6");

            Debug.WriteLine($"Serial Number: {serialNumber}");
            return serialNumber;
        }

        public string ReadInternalSerialNumber()
        {
            int b4 = ReadRegister(AppConfig.INTERNAL_SN_B0);
            int b5 = ReadRegister(AppConfig.INTERNAL_SN_B1);
            int b6 = ReadRegister(AppConfig.INTERNAL_SN_B2);
            int b7 = ReadRegister(AppConfig.INTERNAL_SN_B3);

            Debug.WriteLine($"Sensor Serial: b7:0x{b7:X2} b6:0x{b6:X2} b5:0x{b5:X2} b4:0x{b4:X2}");

            if (b4 < 0 || b5 < 0 || b6 < 0 || b7 < 0)
                return "Read error";

            long serialValue = ((long)b7 << 24) | ((long)b6 << 16) | ((long)b5 << 8) | (long)b4;
            Debug.WriteLine($"Sensor Serial Number: {serialValue}");

            return serialValue.ToString("D10");
        }

        public bool WriteRegister(byte registerAddress, byte value)
        {
            int response = _u2a.UART_Write(new byte[] { SYNC_BYTE, CMD_WRITE, registerAddress, value, SYNC_BYTE }, 5);
            if (response == 0)
            {
                Debug.WriteLine($"Write reg 0x{registerAddress:X2} = 0x{value:X2}");
                return true;
            }
            return false;
        }

        private bool ProgramPage(byte page, byte[] pageData)
        {
            WriteRegister((byte)AppConfig.EEPROM_PAGE_ADDR, page);

            byte[] writeCmd = new byte[10];
            writeCmd[0] = SYNC_BYTE;
            writeCmd[1] = AppConfig.CMD_BURST_WRITE_CACHE;
            Array.Copy(pageData, 0, writeCmd, 2, 8);
            writeCmd[9] = SYNC_BYTE;
            _u2a.UART_Write(writeCmd, (byte)writeCmd.Length);
            Debug.WriteLine("Burst write cache sent.");

            WriteRegister((byte)AppConfig.EEPROM_CTRL, AppConfig.EEPROM_CTRL_ERASE_AND_PROGRAM);

            for (int i = 0; i < 20; i++)
            {
                int status = ReadRegister((byte)AppConfig.EEPROM_STATUS);
                if (status >= 0 && (status & 0x06) == 0)
                    return true;
                Thread.Sleep(10);
            }

            Debug.WriteLine("ERROR: EEPROM write timed out waiting for status.");
            return false;
        }

        public bool BatchWriteRegisters(Dictionary<byte, byte> targetUpdates)
        {
            const int pageSize = 8;

            var pages = targetUpdates.Keys
                .Select(addr => addr / pageSize)
                .Distinct()
                .OrderBy(p => p);

            foreach (int page in pages)
            {
                int pageStart = page * pageSize;
                Debug.WriteLine($"Writing to Page 0x{page:X2} (0x{pageStart:X2}-0x{(pageStart + 7):X2})...");

                byte[] pageData = new byte[pageSize];
                for (int i = 0; i < pageSize; i++)
                {
                    FlushUartRx();
                    int current = ReadRegister((byte)(pageStart + i));
                    if (current < 0)
                    {
                        Debug.WriteLine($"ERROR: Safe back-read failed at 0x{(pageStart + i):X2}");
                        return false;
                    }
                    pageData[i] = (byte)current;
                }

                foreach (var kv in targetUpdates)
                {
                    if (kv.Key >= pageStart && kv.Key < pageStart + pageSize)
                        pageData[kv.Key - pageStart] = kv.Value;
                }

                if (!ProgramPage((byte)page, pageData))
                {
                    Debug.WriteLine($"CRITICAL: Failed to write page 0x{page:X2}");
                    return false;
                }
                Debug.WriteLine($"Page 0x{page:X2} updated successfully.");
            }

            return true;
        }

        public bool BatchWriteCoefficients(Dictionary<string, string> coefficients, string padcGain, string tadcGain, string padcOffset, string tadcOffset)
        {
            var updates = new Dictionary<byte, byte>();

            foreach (var kv in coefficients)
            {
                string key = kv.Key.ToLower();
                if (!AppConfig.COEFFICIENT_ADDRESSES.TryGetValue(key, out byte[]? addrs))
                {
                    Debug.WriteLine($"WARNING: Unknown coefficient '{kv.Key}' — skipped.");
                    continue;
                }
                AddHexBytes(updates, addrs, kv.Value);
            }

            AddHexBytes(updates, AppConfig.PADC_GAIN_ADDR, padcGain);
            AddHexBytes(updates, AppConfig.PADC_OFFSET_ADDR, padcOffset);
            AddHexBytes(updates, AppConfig.TADC_GAIN_ADDR, tadcGain);
            AddHexBytes(updates, AppConfig.TADC_OFFSET_ADDR, tadcOffset);

            return BatchWriteRegisters(updates);
        }

        private void AddHexBytes(Dictionary<byte, byte> updates, byte[] addrs, string hexValue)
        {
            byte lsb = Convert.ToByte(hexValue.Substring(4, 2), 16);
            byte mid = Convert.ToByte(hexValue.Substring(2, 2), 16);
            byte msb = Convert.ToByte(hexValue.Substring(0, 2), 16);

            updates[addrs[0]] = lsb;
            updates[addrs[1]] = mid;
            updates[addrs[2]] = msb;
        }

        public bool TestWriteHCoefficients(byte testValue = 0x01)
        {
            var updates = new Dictionary<byte, byte>();

            string[] hKeys = { "h0", "h1", "h2", "h3" };
            foreach (string key in hKeys)
            {
                byte[] addrs = AppConfig.COEFFICIENT_ADDRESSES[key];

                updates[addrs[0]] = testValue;
                updates[addrs[1]] = testValue;
                updates[addrs[2]] = testValue;
            }

            Debug.WriteLine($"Test writing H coefficients with value 0x{testValue:X2}...");
            return BatchWriteRegisters(updates);
        }
    }
}