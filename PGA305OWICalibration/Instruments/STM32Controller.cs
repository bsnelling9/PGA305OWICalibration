using System;
using System.IO.Ports;
using System.Diagnostics;

namespace PGA305OWICalibration.Instruments
{
    public class STM32Controller
    {
        private SerialPort? _serialPort;
        private byte _currentConfig = 0;

        // Byte mask is the location in the command byte for each control bit sent to the STM32
        private const byte SETOWI_MASK = 0x40;  // PB4  - OWI relay
        private const byte SETMA_MASK = 0x20;  // PA11 - mA relay
        private const byte SETVO_MASK = 0x10;  // PA8  - VO relay
        private const byte MEASRV_MASK = 0x0C;  // PB0  - measure reference voltage
        private const byte MEASVO_MASK = 0x08;  // PA12 - measure voltage output
        private const byte MEASMA_MASK = 0x04;  // PB7  - measure mA
        private const byte VCOMP1_MASK = 0x02;  // PA4  - voltage comparator 1
        private const byte VCOMP0_MASK = 0x01;  // PA5  - voltage comparator 0

        public bool IsConnected => _serialPort?.IsOpen ?? false;

        public bool Open(string portName)
        {
            try
            {
                if (IsConnected) Close();

                _serialPort = new SerialPort(portName)
                {
                    BaudRate = 115200,
                    Parity = Parity.None,
                    StopBits = StopBits.One,
                    DataBits = 8,
                    Handshake = Handshake.None,
                    NewLine = "\n",
                    ReadTimeout = 2000,
                    WriteTimeout = 2000
                };

                _serialPort.Open();
                return _serialPort.IsOpen;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Close()
        {
            _serialPort?.Close();
            _serialPort?.Dispose();
            _serialPort = null;
        }

        private string SendCommand(string command)
        {
            if (!IsConnected || _serialPort == null)
                throw new InvalidOperationException("STM32 is not connected.");
            try
            {
                _serialPort.WriteLine(command);
                return _serialPort.ReadLine().Trim();
            }
            catch (TimeoutException)
            {
                Debug.WriteLine($"STM32 >> {command} << TIMEOUT");
                return "";
            }
        }

        public string GetIdentity() => SendCommand("IDN");

        public byte CurrentConfig => _currentConfig;

        public bool SelectChannel(int channel)
        {
            if (channel < 0 || channel > 7)
                throw new ArgumentOutOfRangeException(nameof(channel));
            Debug.WriteLine($"Selecting STM32 channel {channel}");

            string response = SendCommand($"mx_{channel:X2}");
            return response.Length > 0 && response[0] == 6;
        }

        public bool ConfigureRelays(bool owiRelayClosed, bool maRelayClosed, bool voRelayClosed)
        {
            _currentConfig = (byte)(_currentConfig & ~(SETOWI_MASK | SETMA_MASK | SETVO_MASK));
            if (owiRelayClosed) _currentConfig |= SETOWI_MASK;
            if (maRelayClosed) _currentConfig |= SETMA_MASK;
            if (voRelayClosed) _currentConfig |= SETVO_MASK;

            string response = SendCommand($"cfg{_currentConfig:X2}");
            return response.Length > 0 && response[0] == 6;
        }

        public bool ConfigureVoltageComparators(bool vcompa0High, bool vcompa1High)
        {
            _currentConfig = (byte)(_currentConfig & ~(VCOMP0_MASK | VCOMP1_MASK));
            if (vcompa0High) _currentConfig |= VCOMP0_MASK;
            if (vcompa1High) _currentConfig |= VCOMP1_MASK;

            string response = SendCommand($"cfg{_currentConfig:X2}");
            return response.Length > 0 && response[0] == 6;
        }

        public bool ConfigureMeasurement(bool measRV, bool measVO, bool measMA)
        {
            byte measMask = (byte)(MEASRV_MASK | MEASVO_MASK | MEASMA_MASK);
            _currentConfig = (byte)(_currentConfig & ~measMask);
            if (measRV) _currentConfig |= MEASRV_MASK;
            else if (measVO) _currentConfig |= MEASVO_MASK;
            else if (measMA) _currentConfig |= MEASMA_MASK;

            string response = SendCommand($"cfg{_currentConfig:X2}");
            return response.Length > 0 && response[0] == 6;
        }

        public bool ConnectOWI(int channel)
        {
            if (!SelectChannel(channel)) return false;

            byte vcompBits = (byte)(_currentConfig & (VCOMP0_MASK | VCOMP1_MASK));
            _currentConfig = vcompBits;

            Debug.WriteLine($"ConnectOWI reset cfg: 0x{_currentConfig:X2}");
            string resetResponse = SendCommand($"cfg{_currentConfig:X2}");
            if (resetResponse.Length == 0 || resetResponse[0] != 6) return false;

            _currentConfig |= SETOWI_MASK;
            _currentConfig |= SETVO_MASK;

            Debug.WriteLine($"ConnectOWI final cfg: 0x{_currentConfig:X2}");
            string response = SendCommand($"cfg{_currentConfig:X2}");
            return response.Length > 0 && response[0] == 6;
        }

        public bool DisconnectAll()
        {
            _currentConfig = 0;
            string response = SendCommand($"cfg{_currentConfig:X2}");
            return response.Length > 0 && response[0] == 6;
        }
    }
}
