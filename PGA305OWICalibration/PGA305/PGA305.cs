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

        // EEPROM addresses, why are the EEPRON Memory different?
        public const byte ADDR_PN_LSB = 0x60;
        public const byte ADDR_PN_MID = 0x61;
        public const byte ADDR_PN_MSB = 0x62;

        public const byte ADDR_SERIAL_BYTE0 = 0x64;
        public const byte ADDR_SERIAL_BYTE1 = 0x65;
        public const byte ADDR_SERIAL_BYTE2 = 0x66;
        public const byte ADDR_SERIAL_BYTE3 = 0x67;

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

        private async Task ConsumeResponse()
        {
            using var cts = new CancellationTokenSource(OWI_READ_TIMEOUT_MS);
            try
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    byte[] b = new byte[1];
                    int read = _u2a.UART_Read(b, 1);
                    if (read > 0)
                    {
                        System.Diagnostics.Debug.WriteLine($"  Drain: 0x{b[0]:X2}");
                        if (b[0] == OWI_STOP_BYTE) break;
                    }
                    else
                        await Task.Delay(1, cts.Token);
                }
            }
            catch (OperationCanceledException)
            {
                System.Diagnostics.Debug.WriteLine("DrainBuffer safety timeout");
            }
        }


        public async Task<int> ReadRegister(byte registerAddress)
        {
            // Frame 1 — Read Init
            byte[] owiRequest = new byte[] {
                SYNC_BYTE, CMD_READ_INIT, registerAddress,
                SYNC_BYTE, CMD_READ_RESPONSE
            };
            int writeResponse = _u2a.UART_Write(owiRequest, (byte)owiRequest.Length);
            System.Diagnostics.Debug.WriteLine($"UART response {writeResponse}");

            // Skip 5 echo bytes (3 from frame1 + 2 from frame2)
            int echoSkipped = 0;
            using var echoCts = new CancellationTokenSource(OWI_READ_TIMEOUT_MS);
            try
            {
                while (echoSkipped < 5 && !echoCts.Token.IsCancellationRequested)
                {
                    byte[] dummy = new byte[1];
                    int read = _u2a.UART_Read(dummy, 1);
                    if (read > 0)
                    {
                        System.Diagnostics.Debug.WriteLine($"  Echo skip: 0x{dummy[0]:X2}");
                        echoSkipped++;
                    }
                    else
                        await Task.Delay(1, echoCts.Token);
                }
            }
            catch (OperationCanceledException)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"Echo skip timeout — only skipped {echoSkipped} bytes");
            }

            // Now read actual PGA305 response
            var response = new List<byte>();
            using var cts = new CancellationTokenSource(OWI_READ_TIMEOUT_MS);

            try
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    byte[] oneByte = new byte[1];
                    int read = _u2a.UART_Read(oneByte, 1);

                    if (read > 0)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            $"  RX byte: 0x{oneByte[0]:X2} at index={response.Count}");
                        response.Add(oneByte[0]);

                        if (response.Count >= OWI_MIN_RESPONSE_LEN &&
                            response[response.Count - 2] == OWI_PRE_STOP_BYTE &&
                            response[response.Count - 1] == OWI_STOP_BYTE)
                        {
                            break;
                        }
                    }
                    else
                    {
                        await Task.Delay(1, cts.Token);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"Reg 0x{registerAddress:X2} — safety timeout triggered");
            }

            string rawHex = string.Join(" ", response.Select(b => $"0x{b:X2}"));
            System.Diagnostics.Debug.WriteLine(
                $"Reg 0x{registerAddress:X2} Raw Data: [{rawHex}] (Count: {response.Count})");

            if (response.Count >= OWI_DATA_INDEX + 1)
                return response[OWI_DATA_INDEX];

            return -1;
        }

        public async Task<string> ReadPartNumber()
        {
            int lsb = await ReadRegister(ADDR_PN_LSB);

            int mid = await ReadRegister(ADDR_PN_MID);

            int msb = await ReadRegister(ADDR_PN_MSB);
            System.Diagnostics.Debug.WriteLine($"Part number: msb: {msb} | mid: {mid} | lsb: {lsb}");
            System.Diagnostics.Debug.WriteLine($"Part number: {msb:X2}{mid:X2}{lsb:X2}");

            return (lsb < 0 || mid < 0 || msb < 0) ? "Read error"
            : $"0x{msb:X2}{mid:X2}{lsb:X2}";
        }

        public async Task<string> ReadSerialNumber()
        {
            int b0 = await ReadRegister(ADDR_SERIAL_BYTE0);

            int b1 = await ReadRegister(ADDR_SERIAL_BYTE1);

            int b2 = await ReadRegister(ADDR_SERIAL_BYTE2);     
            int b3 = await ReadRegister(ADDR_SERIAL_BYTE3);

            System.Diagnostics.Debug.WriteLine($"Serial Number: {b3:X2}{b2:X2}{b1:X2}{b0:X2}");

            return (b0 < 0 || b1 < 0 || b2 < 0 || b3 < 0)
                ? "Read error"
                : $"{b3:X2}{b2:X2}{b1:X2}{b0:X2}";
        }
    }
}



/*public async Task<bool> ActivateAsync()
       {
           System.Diagnostics.Debug.WriteLine("Activate() called");

           for (int i = 0; i < 3; i++)
           {
               _u2a.OneWire_SetOutput(1);

               _u2a.OneWire_SetOutput(0);
           }

           byte[] unlockReg8 = new byte[] { 0x55, 0x21, 0x08, 0x55 };
           byte[] unlockReg9 = new byte[] { 0x55, 0x21, 0x09, 0x55 };

           _u2a.UART_Write(unlockReg8, (byte)unlockReg8.Length);

           _u2a.UART_Write(unlockReg9, (byte)unlockReg9.Length);

           System.Diagnostics.Debug.WriteLine("Activate complete - 3 Pulses sent and Interface Unlocked");
           return true;
       }*/