using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace PGA305OWICalibration.Instruments
{
    public enum Power_3V3 { OFF = 0, ON = 1 }
    public enum Power_5V0 { OFF = 0, ON = 1 }
    public enum DACs_WhichDAC { DAC0 = 0, DAC1 = 1 }
    public enum DACs_OperatingMode { Normal = 0, PWD_1k = 1, PWD_100k = 2, PWD_HiZ = 3 }
    public enum UART_BaudRate { _9600_bps = 0, _19200_bps = 1, _38400_bps = 2, _57600_bps = 3, _115200_bps = 4, _230400_bps = 5, _2400_bps = 10, _4800_bps = 11 }
    public enum UART_Parity { None = 0, Even = 1, Odd = 2 }
    public enum UART_BitDirection { LSB_First = 0, MSB_First = 1 }
    public enum UART_CharacterLength { _8_Bit = 0, _7_Bit = 1 }
    public enum UART_StopBits { One = 0, Two = 1 }

    public class USB2AnyDevice
    {
        private int _handle;
        private bool _isOpen = false;

        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aFindControllers();
        public int FindControllers() => u2aFindControllers();

        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aGetSerialNumber(int index, StringBuilder serialNumber);
        public string GetSerialNumber(int index)
        {
            var sb = new StringBuilder(40);
            int ret = u2aGetSerialNumber(index, sb);
            return ret >= 0 ? sb.ToString() : "";
        }

        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aOpenW([MarshalAs(UnmanagedType.LPWStr)] string serialNumber);
        public bool Open(string serialNumber = "")
        {
            _handle = u2aOpenW(serialNumber ?? "");
            _isOpen = _handle > 0;
            if (_isOpen) u2aSetReceiveTimeout(20);
            return _isOpen;
        }

        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aClose(int handle);
        public void Close()
        {
            if (_isOpen)
            {
                u2aClose(_handle);
                _isOpen = false;
            }
        }

        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aSetReceiveTimeout(int milliseconds);
        public int SetReceiveTimeout(int ms) => u2aSetReceiveTimeout(ms);

        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern bool u2aEnableDebugLogging(bool enable);
        public void EnableDebugLogging() => u2aEnableDebugLogging(true);

        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aStatus_GetText(int code, [MarshalAs(UnmanagedType.LPArray)] byte[] buffer, int bufsize);
        public void GetStatusText(int code, ref string text)
        {
            byte[] buffer = new byte[64];
            u2aStatus_GetText(code, buffer, 64);
            text = ASCIIEncoding.ASCII.GetString(buffer);
        }

        public int GetHandle() => _handle;


        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aPower_WriteControl(int handle, Power_3V3 p33, Power_5V0 p50);
        public int Power_WriteControl(Power_3V3 p33, Power_5V0 p50) => u2aPower_WriteControl(_handle, p33, p50);

    
        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aDACs_Write(int handle, DACs_WhichDAC dac, DACs_OperatingMode mode, byte value);
        public int DACs_Write(DACs_WhichDAC dac, DACs_OperatingMode mode, byte value) => u2aDACs_Write(_handle, dac, mode, value);


        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aOneWire_SetMode(int handle, ushort mode);
        public int OneWire_SetMode(ushort mode) => u2aOneWire_SetMode(_handle, mode);

        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aOneWire_PulseSetup(int handle, ushort setup, ushort low, ushort high, ushort store, int flags);
        public int OneWire_PulseSetup(ushort setup, ushort low, ushort high, ushort store, int flags) => u2aOneWire_PulseSetup(_handle, setup, low, high, store, flags);

        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aOneWire_PulseWriteEx(int handle, byte address, ushort pulses);
        public int OneWire_PulseWriteEx(byte address, ushort pulses) => u2aOneWire_PulseWriteEx(_handle, address, pulses);

        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aOneWire_SetOutput(int handle, byte state);
        public int OneWire_SetOutput(byte state) => u2aOneWire_SetOutput(_handle, state);


        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aUART_Control(int handle, UART_BaudRate baud, UART_Parity parity, UART_BitDirection bitDir, UART_CharacterLength charLen, UART_StopBits stopBits);
        public int UART_Control() => u2aUART_Control(_handle, UART_BaudRate._9600_bps, UART_Parity.None, UART_BitDirection.LSB_First, UART_CharacterLength._8_Bit, UART_StopBits.One);

        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aUART_SetMode(int handle, uint mode);
        public int UART_SetMode(uint mode) => u2aUART_SetMode(_handle, mode);

        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aUART_Write(int handle, byte nBytes, [MarshalAs(UnmanagedType.LPArray)] byte[] data);
        public int UART_Write(byte[] data, byte nBytes) => u2aUART_Write(_handle, nBytes, data);

        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aUART_Read(int handle, byte nBytes, [MarshalAs(UnmanagedType.LPArray)] byte[] buffer);
        public int UART_Read(byte[] buffer, byte length)
        {
            int result = u2aUART_Read(_handle, length, buffer);
            Debug.WriteLine($"UART_Read raw result: {result}");
            return result;
        }

        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aUART_GetRxCount(int handle);
        public int UART_GetRxCount() => u2aUART_GetRxCount(_handle);

        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aUART_DisableReceiver(int handle);
        public int UART_DisableReceiver() => u2aUART_DisableReceiver(_handle);
  
       
        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aGPIO_SetPort(int handle, byte port, byte function);
        public int GPIO_SetPort(byte port, byte function) => u2aGPIO_SetPort(_handle, port, function);

        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aGPIO_WritePort(int handle, byte port, byte state);
        public int GPIO_WritePort(byte port, byte state) => u2aGPIO_WritePort(_handle, port, state);

        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aGPIO_WritePulse(int handle, byte port, byte polarity, ushort duration);
        public int GPIO_WritePulse(byte port, byte polarity, ushort duration) => u2aGPIO_WritePulse(_handle, port, polarity, duration);


        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aI2C_Control(int handle, int speed, int addressLength, int pullUps);
        public int I2C_Control(int speed, int addressLength, int pullUps) => u2aI2C_Control(_handle, speed, addressLength, pullUps);

        [DllImport("USB2ANY_2.8.2.dll")]
        private static extern int u2aI2C_RegisterWrite(int handle, ushort i2cAddress, byte registerAddress, byte value);
        public int I2C_RegisterWrite(ushort i2cAddress, byte registerAddress, byte value) => u2aI2C_RegisterWrite(_handle, i2cAddress, registerAddress, value);

    }
}