using PGA305OWICalibration.Config;
using PGA305OWICalibration.Instruments;
using System.Diagnostics;

namespace PGA305OWICalibration.PGA305
{
    public class PGA305Device
    {
        private readonly USB2AnyDevice _u2a;
        public PGA305Device(USB2AnyDevice device) => _u2a = device;

        public void ParkLines()
        {
            _u2a.GPIO_SetPort(AppConfig.GPIO10, AppConfig.FN_OUTPUT);
            _u2a.GPIO_WritePort(AppConfig.GPIO10, AppConfig.STATE_LOW);
            _u2a.GPIO_SetPort(AppConfig.GPIO11, AppConfig.FN_OUTPUT);
            _u2a.GPIO_WritePort(AppConfig.GPIO11, AppConfig.STATE_LOW);
        }

        public bool Initialize()
        {
            Debug.WriteLine("Initialise() called");

            int result = _u2a.OneWire_SetMode(AppConfig.OW_MODE);
            
            if (result < 0) return false;

            _u2a.OneWire_SetOutput(0);
            _u2a.SetReceiveTimeout(25);

            int uartResult = _u2a.UART_Control();

            int modeResult = _u2a.UART_SetMode(2);

            ParkLines();

            return true;
        }

        //Did this and was measureing 6.4V on the pin so the activate signal level is correect.
        // I believe it has to be above 5.8V
        public void ActivatePinHigh()
        {
            _u2a.GPIO_SetPort(AppConfig.GPIO7, AppConfig.FN_OUTPUT);
            _u2a.GPIO_WritePort(AppConfig.GPIO7, AppConfig.STATE_HIGH);
        }

        public bool Activate()
        {
            Debug.WriteLine("Activate called OWI");
            byte[] response = new byte[54];

            _u2a.OneWire_PulseSetup(AppConfig.TIME_SETUP, AppConfig.ACT_TIME_LOW, AppConfig.ACT_TIME_HIGH, AppConfig.TIME_STORE, AppConfig.FLAGS);

            _u2a.OneWire_PulseWriteEx(0, 2);
            Thread.Sleep(25);
            _u2a.OneWire_PulseWriteEx(0, 2);

            _u2a.GPIO_WritePort(AppConfig.GPIO11, AppConfig.STATE_HIGH);

            _u2a.UART_Write(new byte[] {
                AppConfig.SYNC_BYTE, AppConfig.CMD_WRITE_PAGE0, 0x08, AppConfig.SYNC_BYTE,
                AppConfig.SYNC_BYTE, AppConfig.CMD_WRITE_PAGE0, 0x09, AppConfig.SYNC_BYTE
            }, 8);

            _u2a.UART_Write(new byte[] { AppConfig.SYNC_BYTE, 0x02, 0x0C, AppConfig.SYNC_BYTE, AppConfig.CMD_READ_RESPONSE }, 5);
            int count = _u2a.UART_Read(response, 54);

            Debug.WriteLine($"Activate: got {count} bytes");            

            if (count > 0 && response[count - 1] == 0x03)
            {
                Debug.WriteLine("Device entered Command mode.");
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
            
            _u2a.UART_Write(new byte[] { AppConfig.SYNC_BYTE, AppConfig.CMD_READ_INIT, registerAddress, AppConfig.SYNC_BYTE, AppConfig.CMD_READ_RESPONSE }, 5);
            
            int count = _u2a.UART_Read(response, 54);                        
            
            //this works as long as UART.SET_MODE is set to 2 (RecvAfterXmit)
            return response[0];
        }

        public string ReadPressureCode()
        {
            int lsb = ReadRegister(EEPROMRegister.PRANGE_LSB);
            int msb = ReadRegister(EEPROMRegister.PRANGE_MSB);
            int accuracyByte = ReadRegister(EEPROMRegister.ACCURACY);
            
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
            int lsb = ReadRegister(EEPROMRegister.SENSOR_SN_B0);
            int mid = ReadRegister(EEPROMRegister.SENSOR_SN_B1);
            int msb = ReadRegister(EEPROMRegister.SENSOR_SN_B2);
            
            Debug.WriteLine($"Sensor serial: msb:0x{msb:X2} mid:0x{mid:X2} lsb:0x{lsb:X2}");
            
            if (lsb < 0 || mid < 0 || msb < 0) return "Read error";
            
            int serialValue = lsb + (mid << 8) + (msb << 16);
            string serialNumber = serialValue.ToString("D6");
            
            Debug.WriteLine($"Serial Number: {serialNumber}");
            return serialNumber;
        }

        public int ReadInternalSerialNumber()
        {
            //maybe use the cache to read the internal serial number and ignore the 0-3 bytes instead
            // also there my need to be a wait or something so that this comes back clean, becuase right now when I read everything it is messy.
            int b4 = ReadRegister(EEPROMRegister.INTERNAL_SN_B0);
            int b5 = ReadRegister(EEPROMRegister.INTERNAL_SN_B1);
            int b6 = ReadRegister(EEPROMRegister.INTERNAL_SN_B2);
            int b7 = ReadRegister(EEPROMRegister.INTERNAL_SN_B3);

            Debug.WriteLine($"Transducer Serial: b7:0x{b7:X2} b6:0x{b6:X2} b5:0x{b5:X2} b4:0x{b4:X2}");

            if (b4 < 0 || b5 < 0 || b6 < 0 || b7 < 0)
            { 
                //add error message
                return -1;            
            }                           

            int serialNumber = (b7 << 24) | (b6 << 16) | (b5 << 8) | b4;
            Debug.WriteLine($"Transducer Serial Number: {serialNumber}");

            return serialNumber;
        }

        public bool WriteRegister(byte registerAddress, byte value)
        {
            int response = _u2a.UART_Write(new byte[] { AppConfig.SYNC_BYTE, AppConfig.CMD_WRITE, registerAddress, value }, 4);
            if (response == 0)
            {
                Debug.WriteLine($"Write reg 0x{registerAddress:X2} = 0x{value:X2}");
                return true;
            }
            
            return false;
        }

        public bool ProgramDevice(
            Dictionary<string, string> coefficients,
            Dictionary<byte, byte> analogRegisters)
        {
            if (!WriteCoefficients(coefficients))
            {
                Debug.WriteLine("ERROR: coefficient write failed.");
                return false;
            }
            if (analogRegisters.Count > 0 && !BatchWriteRegisters(analogRegisters))
            {
                Debug.WriteLine("ERROR: analog register write failed.");
                return false;
            }
            
            byte crc = GetCRCValue();
            
            if (!BatchWriteRegisters(new Dictionary<byte, byte>
            { { (byte)EEPROMRegister.PAGE_F_CRC, crc } }))
            {
                Debug.WriteLine("ERROR: CRC write failed.");
                return false;
            }
            Debug.WriteLine($"Device programmed. CRC = 0x{crc:X2}");
            return true;
        }

        public bool WriteCoefficients(Dictionary<string, string> coefficients)
        {
            const int pageSize = EEPROMRegister.EEPROM_PAGE_SIZE;
            var targetUpdates = new Dictionary<byte, byte>();
            
            foreach (var coefficient in coefficients)
            {
                if (!EEPROMRegister.COEFFICIENT_ADDRESSES.TryGetValue(
                        coefficient.Key, out byte[]? addresses))
                {
                    Debug.WriteLine($"ERROR: Unknown coefficient '{coefficient.Key}'.");
                    return false;
                }
                string hex = coefficient.Value;
                if (hex.Length != 6)
                {
                    Debug.WriteLine(
                        $"ERROR: Coefficient {coefficient.Key} has invalid value '{hex}'.");
                    return false;
                }
                byte msb = Convert.ToByte(hex.Substring(0, 2), 16);
                byte mid = Convert.ToByte(hex.Substring(2, 2), 16);
                byte lsb = Convert.ToByte(hex.Substring(4, 2), 16);
                
                targetUpdates[addresses[0]] = lsb;
                targetUpdates[addresses[1]] = mid;
                targetUpdates[addresses[2]] = msb;
                
                Debug.WriteLine(
                    $"{coefficient.Key}: {hex} -> " +
                    $"LSB=0x{lsb:X2}, MID=0x{mid:X2}, MSB=0x{msb:X2}");
            }
            var pages = targetUpdates.Keys
                .Select(address => address / pageSize)
                .Distinct()
                .OrderBy(page => page);
            
            foreach (int page in pages)
            {
                int pageStart = page * pageSize;
                Debug.WriteLine(
                    $"Writing coefficient Page 0x{page:X2} " +
                    $"(0x{pageStart:X2}-0x{pageStart + 7:X2})...");
                byte[] pageData = new byte[pageSize];
                foreach (var update in targetUpdates)
                {
                    if (update.Key >= pageStart &&
                        update.Key < pageStart + pageSize)
                    {
                        pageData[update.Key - pageStart] = update.Value;
                    }
                }
                if (!WriteEEPROMpage((byte)page, pageData))
                {
                    Debug.WriteLine(
                        $"ERROR: Failed to write coefficient page 0x{page:X2}.");
                    return false;
                }
            }
            return true;
        }

        public byte GetCRCValue()
        {
            WriteRegister(EEPROMRegister.EEPROM_CRC_TRIG, 0x01);
            byte newCRC = (byte)ReadRegister(EEPROMRegister.EEPROM_CRC_VAL);
            return newCRC;
        }

        private bool WriteEEPROMpage(byte page, byte[] pageData)
        {
            const int pageSize = EEPROMRegister.EEPROM_PAGE_SIZE;
            if (pageData.Length != pageSize)
            {
                Debug.WriteLine($"ERROR: EEPROM page must contain exactly {pageSize} bytes.");
                return false;
            }
           
            byte[] cmd = new byte[18];
            
            cmd[0] = AppConfig.SYNC_BYTE;
            cmd[1] = AppConfig.CMD_WRITE;
            cmd[2] = (byte)EEPROMRegister.EEPROM_PAGE_ADDR;
            cmd[3] = page;
            cmd[4] = AppConfig.SYNC_BYTE;
            cmd[5] = EEPROMRegister.CMD_BURST_WRITE_CACHE;
            Array.Copy(pageData, 0, cmd, 6, pageSize);
            cmd[14] = AppConfig.SYNC_BYTE;
            cmd[15] = AppConfig.CMD_WRITE;
            cmd[16] = (byte)EEPROMRegister.EEPROM_CTRL;
            cmd[17] = (byte)EEPROMRegister.EEPROM_CTRL_ERASE_AND_PROGRAM;
            
            _u2a.UART_Write(cmd, (byte)cmd.Length);
            Debug.WriteLine($"Page 0x{page:X2} cache: {string.Join(" ", pageData.Select(b => $"0x{b:X2}"))}");
            
            Thread.Sleep(15);
            byte[] discard = new byte[54];
            int junk = _u2a.UART_Read(discard, 54);
            
            if (junk > 0) Debug.WriteLine($"Discarded {junk} program-cycle byte(s)");
            int pageStart = page * pageSize;
            
            for (int i = 0; i < pageSize; i++)
            {
                int actual = ReadRegister((byte)(pageStart + i));
                if (actual < 0)
                {
                    Debug.WriteLine($"ERROR: read failed at 0x{pageStart + i:X2}.");
                    return false;
                }
                if ((byte)actual != pageData[i])
                {
                    Debug.WriteLine($"ERROR: verify failed at 0x{pageStart + i:X2}. " +
                                    $"Expected 0x{pageData[i]:X2}, got 0x{actual:X2}.");
                    return false;
                }
            }
            Debug.WriteLine($"EEPROM page 0x{page:X2} written and verified.");
            return true;
        }

        public bool BatchWriteRegisters(Dictionary<byte, byte> targetUpdates)
        {
            const int pageSize = EEPROMRegister.EEPROM_PAGE_SIZE;
            
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
                if (!WriteEEPROMpage((byte)page, pageData))
                {
                    Debug.WriteLine($"CRITICAL: Failed to write page 0x{page:X2}");
                    return false;
                }
                Debug.WriteLine($"Page 0x{page:X2} updated successfully.");
            }
            return true;
        }

        public byte[] ReadEepromCache()
        {
            byte[] flush = new byte[54];
            byte[] data = new byte[54];

            _u2a.UART_Write(new byte[] { AppConfig.SYNC_BYTE, AppConfig.CMD_READ_INIT, EEPROMRegister.PRANGE_LSB }, 3);
            _u2a.UART_Read(flush, 54);

            _u2a.UART_Write(new byte[] { AppConfig.SYNC_BYTE, AppConfig.CMD_READ_RESPONSE }, 2);
            _u2a.UART_Read(data, 54);

            for (int i = 0; i < 8; i++)
                Debug.WriteLine($"  cache[{i}] = 0x{data[i]:X2}");

            return data;
        }
    }
}