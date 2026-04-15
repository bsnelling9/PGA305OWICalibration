using System.Diagnostics;
using System.Runtime.InteropServices;
using TI.eLAB.EVM;

namespace PGA305OWICalibration.Instruments
{
    public class USB2AnyDevice
    {
        private bool _isOpen = false;
        private U2A _u2a = new U2A();

        [DllImport("USB2ANY.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "u2aUART_GetRxCount")]
        private static extern int NativeUART_GetRxCount(int handle);

        public int UART_GetRxCount() => NativeUART_GetRxCount(_u2a.u2aHandle);

        [DllImport("USB2ANY.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "u2aUART_SetMode")]
        private static extern int NativeUART_SetMode(int handle, uint mode);

        public int UART_SetMode(uint mode) => NativeUART_SetMode(_u2a.u2aHandle, mode);

        [DllImport("USB2ANY.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "u2aUART_DisableReceiver")]
        private static extern int NativeUART_DisableReceiver(int handle);

        public int UART_DisableReceiver() => NativeUART_DisableReceiver(_u2a.u2aHandle);


        public int GetHandle() => _u2a.u2aHandle;
        public int FindControllers() => _u2a.FindControllers();

        public string GetSerialNumber(int index)
        {
            string serial = "";
            int result = _u2a.GetSerialNumber(index, ref serial);
            return (result >= 0) ? serial : "";
        }

        public bool Open(string serialNumber = "")
        {
            int result = _u2a.Open(serialNumber ?? "");
            _isOpen = _u2a.u2aHandle > 0;
            return _isOpen;
        }

        public void Close()
        {
            if (_isOpen) 
            { 
                _u2a.Close(); 
                _isOpen = false; 
            }
        }

        public int Power_WriteControl(Power_3V3 p33, Power_5V0 p50) => _u2a.Power_WriteControl(p33, p50);
               
        public int DACs_Write(DACs_WhichDAC dac, DACs_OperatingMode mode, byte value)
            => _u2a.DACs_Write(dac, mode, value);

        public int OneWire_SetMode(ushort mode) => _u2a.OneWire_SetMode(mode);

        
        public int OneWire_PulseSetup(ushort setup, ushort low, ushort high, ushort store, int flags)
            => _u2a.OneWire_PulseSetup(setup, low, high, store, flags);

        public int OneWire_PulseWriteEx(byte address, ushort pulses) => _u2a.OneWire_PulseWriteEx(address, pulses);

        public int OneWire_SetOutput(byte state) => _u2a.OneWire_SetOutput(state);

        public int UART_Read(byte[] buffer, byte length)
        {
            int result = _u2a.UART_Read(length, buffer);
            Debug.WriteLine($"UART_Read raw result: {result}");
            return result;
        }

        public int UART_Write(byte[] data, byte nBytes) => _u2a.UART_Write(nBytes, data);

        public int UART_Control()
        {
            return _u2a.UART_Control(
                UART_BaudRate._9600_bps,
                UART_Parity.None,
                UART_BitDirection.LSB_First,
                UART_CharacterLength._8_Bit,
                UART_StopBits.One);
        }

        public void EnableDebugLogging() => _u2a.EnableDebugLogging(true);

        public int SetReceiveTimeout(int milliseconds) => _u2a.SetReceiveTimeout(milliseconds);

        public void GetStatusText(int code, ref string text) => _u2a.Status_GetText(code, ref text);

        public int GPIO_SetPort(byte port, byte function) => _u2a.GPIO_SetPort(port, function);

        public int GPIO_WritePulse(byte port, byte polarity, ushort duration)
            => _u2a.GPIO_WritePulse(port, polarity, duration);

        public int GPIO_WritePort(byte port, byte state) => _u2a.GPIO_WritePort(port, state);
    }
}
