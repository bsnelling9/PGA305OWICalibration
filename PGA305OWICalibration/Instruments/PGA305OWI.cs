using PGA305OWICalibration.Instruments;
using System.Diagnostics;
using TI.eLAB.EVM;

namespace PGA305OWICalibration.PGA305EVM
{
    public class PGA305Owi
    {
        private USB2AnyDevice _u2a;
        //NOTE: Move these to common folder and make public later
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
        private const byte CMD_WRITE = 0x51;
        private const byte CMD_READ_INIT = 0x52;
        private const byte CMD_READ_RESPONSE = 0x73;

        // EEPROM addresses
        public const byte ADDR_PN_LSB = 0x70;
        public const byte ADDR_PN_MID = 0x71;
        public const byte ADDR_PN_MSB = 0x72;
        public const byte ADDR_SERIAL_BYTE0 = 0x60;
        public const byte ADDR_SERIAL_BYTE1 = 0x61;
        public const byte ADDR_SERIAL_BYTE2 = 0x62;
        public const byte ADDR_SERIAL_BYTE3 = 0x63;
        public const byte ADDR_SERIAL_BYTE4 = 0x64;
        public const byte ADDR_SERIAL_BYTE5 = 0x65;
        public const byte ADDR_SERIAL_BYTE6 = 0x66;
        public const byte ADDR_SERIAL_BYTE7 = 0x67;

        // OWI response frame structure (confirmed from Saleae captures)
        // [echo] [0xF6] [DATA] [0xFF] [0xF7]
        // Data is always at index 2
        // Stop sequence is always 0xFF 0xF7
        private const byte OWI_STOP_BYTE = 0xF7;
        private const byte OWI_PRE_STOP_BYTE = 0xFF;
        private const int OWI_DATA_INDEX = 2;
        private const int OWI_MIN_RESPONSE_LEN = 5;
        private const int OWI_READ_TIMEOUT_MS = 500;


        public PGA305Owi(USB2AnyDevice device) => _u2a = device;
  
        public bool Initialize()
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

            //_u2a.UART_SetMode(2);
            _u2a.SetReceiveTimeout(25);

            Thread.Sleep(20);

            _u2a.GPIO_SetPort(GPIO7, FN_OUTPUT);
            _u2a.GPIO_WritePort(GPIO7, STATE_LOW);

            _u2a.GPIO_SetPort(GPIO11, FN_OUTPUT);
            _u2a.GPIO_WritePort(GPIO11, STATE_HIGH);

            _u2a.GPIO_SetPort(GPIO10, FN_OUTPUT);
            _u2a.GPIO_WritePort(GPIO10, STATE_LOW);

            _u2a.DACs_Write(DAC_OWI_OFFSET, DACs_OperatingMode.Normal, DAC_OFFSET_ZERO);
            Thread.Sleep(10);

            return true;
        }

        public bool Activate()
        {
            Debug.WriteLine("Activate() called");

            _u2a.OneWire_PulseSetup(TIME_SETUP, ACT_TIME_LOW, ACT_TIME_HIGH, TIME_STORE, FLAGS);

            int result = _u2a.OneWire_PulseWriteEx(0, 2);
            Debug.WriteLine($"OneWire_PulseWriteEx result: {result}");

            byte[] unlockReg8 = new byte[] { 0x55, 0x01, 0x08, 0x55 };
            byte[] unlockReg9 = new byte[] { 0x55, 0x01, 0x09, 0x55 };

            //_u2a.OneWire_PulseSetup(TIME_SETUP, TIME_LOW, TIME_HIGH, TIME_STORE, FLAGS);

            _u2a.UART_Control();
            //_u2a.UART_SetMode(2);
            _u2a.UART_Write(unlockReg8, (byte)unlockReg8.Length);
            
            _u2a.UART_Write(unlockReg9, (byte)unlockReg9.Length);


           Debug.WriteLine("Activate complete - 3 Pulses sent and Interface Unlocked");
            return true;
        }

        public void LoadEepromCache(byte page)
        {
            byte[] buffer = new byte[54];
            _u2a.UART_Write(new byte[] { SYNC_BYTE, CMD_WRITE, 0x88, page }, 4);
            _u2a.UART_Write(new byte[] { SYNC_BYTE, CMD_WRITE, 0x89, 0x01 }, 4);
            _u2a.UART_Read(buffer, 54);
            Debug.WriteLine($"EEPROM cache loaded for page {page}");
        }

        /*public async Task LoadEepromCache(byte page)
        {
            await Task.Run(() =>
            {
                byte[] return_data = new byte[54];

                _u2a.UART_Write(new byte[] { SYNC_BYTE, CMD_WRITE, 0x88, page }, 4);
                _u2a.UART_Write(new byte[] { SYNC_BYTE, CMD_WRITE, 0x89, 0x01 }, 4);
                _u2a.UART_Read(return_data, 54); 
                Debug.WriteLine($"EEPROM cache loaded for page {page}");
            });
        }*/



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

                // Step 3: Poll with safety exit
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


            _u2a.UART_Write(new byte[] { SYNC_BYTE, CMD_READ_INIT, registerAddress }, 3);
            _u2a.UART_Read(flush, 54);

            _u2a.UART_Write(new byte[] { SYNC_BYTE, CMD_READ_RESPONSE }, 2);
            _u2a.UART_Read(response, 54);

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
            return $"0x{msb:X2}{mid:X2}{lsb:X2}";
        }

        public string ReadSerialNumber()
        {
            int b0 = ReadRegister(ADDR_SERIAL_BYTE0);
            int b1 = ReadRegister(ADDR_SERIAL_BYTE1);
            int b2 = ReadRegister(ADDR_SERIAL_BYTE2);
            int b3 = ReadRegister(ADDR_SERIAL_BYTE3);
            int b4 = ReadRegister(ADDR_SERIAL_BYTE4);
            int b5 = ReadRegister(ADDR_SERIAL_BYTE5);
            int b6 = ReadRegister(ADDR_SERIAL_BYTE6);
            int b7 = ReadRegister(ADDR_SERIAL_BYTE7);

            Debug.WriteLine($"Serial Number: b7: {b7} | b6: {b6} | b5: {b5} | b4: {b4} | b3: {b3} | b2: {b2} | b1: {b1} | b0: {b0}");

            Debug.WriteLine($"Serial Number: b7:0x{b7:X2} b6:0x{b6:X2} b5:0x{b5:X2} b4:0x{b4:X2} b3:0x{b3:X2} b2:0x{b2:X2} b1:0x{b1:X2} b0:0x{b0:X2}");

            /*if (b0 < 0 || b1 < 0 || b2 < 0 || b3 < 0 || b4 < 0 || b5 < 0 || b6 < 0 || b7 < 0) return "Read error";
            return $"{b7:X2}{b6:X2}{b5:X2}{b4:X2}{b3:X2}{b2:X2}{b1:X2}{b0:X2}";*/

            if (b0 < 0 || b1 < 0 || b2 < 0 || b3 < 0 || b4 < 0 || b5 < 0 || b6 < 0 || b7 < 0)
                return "Read error";

            // Combine all bytes into a single integer value
            long serialValue = ((long)b7 << 56) | ((long)b6 << 48) | ((long)b5 << 40) | ((long)b4 << 32) |
                               ((long)b3 << 24) | ((long)b2 << 16) | ((long)b1 << 8) | (long)b0;
            Debug.WriteLine($"{serialValue}");

            return serialValue.ToString("D6");
        }


        /*
         * Python code for reading the Part Number and serial number using I2C
         *     def read_sensor_data(self, channel: int, verbose=True) -> Optional[Dict]:
        """Read Part Number, Serial Number, and PRange from a PGA305 sensor."""
        if verbose:
            print(f"\nReading channel {channel}...")

        self.set_channel(channel)

        if not self.enter_command_mode():
            if verbose:
                print("ERROR: Could not enter command mode")
                print("  Try power cycling the board")
            return None

        if verbose:
            print("Command mode active")

        if self.register_map:
            pn_addrs = [self.register_map.get(f'EEPROM_ARRAY PN_{s}', d)
                        for s, d in [('LSB', 0x70), ('MID', 0x71), ('MSB', 0x72)]]
            sn_addrs = [self.register_map.get(f'EEPROM_ARRAY SN_{s}', d)
                        for s, d in [('LSB', 0x73), ('MID', 0x74), ('MSB', 0x75)]]
            pr_addrs = [self.register_map.get(f'EEPROM_ARRAY PRANGE_{s}', d)
                        for s, d in [('LSB', 0x76), ('MSB', 0x77)]]
        else:
            pn_addrs = [0x70, 0x71, 0x72]
            sn_addrs = [0x73, 0x74, 0x75]
            pr_addrs = [0x76, 0x77]

        pn_bytes = [self.read_register(a, config.EEPROM_ADDR) for a in pn_addrs]
        if None in pn_bytes:
            if verbose:
                print("ERROR: Failed to read Part Number")
            self.disconnect_channel()
            return None

        pn_lsb, pn_mid, pn_msb = pn_bytes
        prefix = "A" if (pn_msb % 128) == 0 else "S"
        pn_numeric = pn_lsb + (pn_mid << 8) + ((pn_msb // 128) << 16)
        part_number = prefix + str(pn_numeric)

        sn_bytes = [self.read_register(a, config.EEPROM_ADDR) for a in sn_addrs]
        if None in sn_bytes:
            if verbose:
                print("ERROR: Failed to read Serial Number")
            self.disconnect_channel()
            return None

        serial_number = sn_bytes[0] + (sn_bytes[1] << 8) + (sn_bytes[2] << 16)

        pr_bytes = [self.read_register(a, config.EEPROM_ADDR) for a in pr_addrs]
        if None in pr_bytes:
            prange = None
        else:
            prange = pr_bytes[0] + (pr_bytes[1] << 8)

        self.disconnect_channel()

        return {
            'part_number': part_number,
            'serial_number': serial_number,
            'prange': prange
        }*/
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

        public async Task<string> ReadSerialNumberAsync()
        {
            int b0 = await ReadRegisterAsync(ADDR_SERIAL_BYTE0);
            
            int b1 = await ReadRegisterAsync(ADDR_SERIAL_BYTE1);
            
            int b2 = await ReadRegisterAsync(ADDR_SERIAL_BYTE2);
            
            int b3 = await ReadRegisterAsync(ADDR_SERIAL_BYTE3);

            Debug.WriteLine($"Serial Number: b3: {b3} | b2: {b2} | b1: {b1} | b0: {b0}");

            Debug.WriteLine($"Serial Number: {b3:X2}{b2:X2}{b1:X2}{b0:X2}");
            
            return (b0 < 0 || b1 < 0 || b2 < 0 || b3 < 0)
                ? "Read error"
                : $"{b3:X2}{b2:X2}{b1:X2}{b0:X2}";

        }
    }
}