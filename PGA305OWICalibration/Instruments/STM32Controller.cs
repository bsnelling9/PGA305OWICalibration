using PGA305OWICalibration.Config;
using System.Diagnostics;
using System.IO.Ports;

namespace PGA305OWICalibration.Instruments
{
    public class STM32Controller
    {
        private SerialPort? _serialPort;
        private byte _currentConfig = 0;
        public bool IsConnected => _serialPort?.IsOpen ?? false;

        public bool Open(string portName)
        {
            try
            {
                if (IsConnected) Close();
                //move these to the config file
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

        public string SendCommand(string command)
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

        public bool ConfigurePowerRelays(bool owiRelayClosed, bool maRelayClosed, bool voRelayClosed)
        {
            _currentConfig = (byte)(_currentConfig & ~MuxSTM32Config.RelayMask);
            
            if (owiRelayClosed) 
                _currentConfig |= MuxSTM32Config.OwiRelayBit;
            if (maRelayClosed) 
                _currentConfig |= MuxSTM32Config.MaRelayBit;
            if (voRelayClosed) 
                _currentConfig |= MuxSTM32Config.VoRelayBit;

            Debug.WriteLine($"Configure Relay:cfg{_currentConfig:X2}");

            string response = SendCommand($"cfg{_currentConfig:X2}");

            return response.Length > 0 && response[0] == 6;
        }

        public bool ConfigureVoltageComparators(bool vcompa0High, bool vcompa1High)
        {
            _currentConfig = (byte)(_currentConfig & ~MuxSTM32Config.ComparatorMask);
            if (vcompa0High) 
                _currentConfig |= MuxSTM32Config.VCompA0Bit;
            if (vcompa1High) 
                _currentConfig |= MuxSTM32Config.VCompA1Bit;

            Debug.WriteLine($"Configure Voltage Comparators cfg: 0x{_currentConfig:X2}");
            string response = SendCommand($"cfg{_currentConfig:X2}");
            return response.Length > 0 && response[0] == 6;
        }

        public bool ConfigureMeasurementRelays(bool measRV, bool measVO, bool measMA)
        {
            byte measMask = (byte)(MuxSTM32Config.MEASRV_MASK | MuxSTM32Config.MeasureVoBit | MuxSTM32Config.MeasureMaBit);
            _currentConfig = (byte)(_currentConfig & ~measMask);

            if (measRV) 
                _currentConfig |= MuxSTM32Config.MEASRV_MASK;
            else if 
                (measVO) 
                _currentConfig |= MuxSTM32Config.MeasureVoBit;
            else if 
                (measMA) 
                _currentConfig |= MuxSTM32Config.MeasureMaBit;

            Debug.WriteLine($"Configure Measurement cfg: 0x{_currentConfig:X2}");
            string response = SendCommand($"cfg{_currentConfig:X2}");
            return response.Length > 0 && response[0] == 6;
        }

        public bool ConfigureMuxForOWI(string signalType)
        {
            if (!MuxSTM32Config.Compensation.TryGetValue(signalType, out var comp))
            {
                Debug.WriteLine($"Compensation for '{signalType}' has not been set up");
                return false;
            }

            if (!ConfigureVoltageComparators(comp.VCompA0High, comp.VCompA1High))
            {
                Debug.WriteLine("Compensation failed");
                return false;
            }

            Thread.Sleep(MuxSTM32Config.ComparatorSettleMs);

            if (!ConfigurePowerRelays(owiRelayClosed: true, maRelayClosed: false, voRelayClosed: true))
            {
                Debug.WriteLine("Relay config failed");
                return false;
            }

            Thread.Sleep(MuxSTM32Config.RelaySettleMs);
            return true;
        }

        public bool DisconnectAll()
        {
            _currentConfig = 0;
            string response = SendCommand($"cfg{_currentConfig:X2}");
            return response.Length > 0 && response[0] == 6;
        }
    }
}