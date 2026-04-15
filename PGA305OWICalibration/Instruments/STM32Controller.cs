using System;
using System.IO.Ports; // This requires the NuGet package "System.IO.Ports"
using System.Threading;

namespace PGA305OWICalibration.Instruments
{
    public class STM32Controller
    {
        
        private SerialPort? _serialPort;
       
        public bool IsConnected => _serialPort != null && _serialPort.IsOpen;

        public bool Open(string portName)
        {
            try
            {
                if (IsConnected) Close();

                _serialPort = new SerialPort(portName)
                {
                    BaudRate = 115200,
                    Parity = System.IO.Ports.Parity.None,
                    StopBits = System.IO.Ports.StopBits.One,
                    DataBits = 8,
                    Handshake = System.IO.Ports.Handshake.None,
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

        private async Task<string> SendCommand(string command)
        {
            if (!IsConnected || _serialPort == null)
                throw new InvalidOperationException("STM32 is not connected.");

            _serialPort.WriteLine(command);

            await Task.Delay(50);

            try
            {
                string response = _serialPort.ReadLine().Trim();
                bool isAck = response.Length > 0 && response[0] == 6;
                bool isNack = response.Length > 0 && response[0] == 15;
                System.Diagnostics.Debug.WriteLine(
                    $"STM32 >> {command} << len={response.Length} " +
                    $"{(isAck ? "ACK" : isNack ? "NACK" : $"raw={((int)response[0])}")}");
                return response;
            }
            catch (TimeoutException)
            {
                System.Diagnostics.Debug.WriteLine($"STM32 >> {command} << TIMEOUT");
                return "";
            }
        }

        public async Task<string> GetIdentity() => await SendCommand("IDN");

        public async Task<bool> SelectChannel(int channel)
        {
            if (channel < 0 || channel > 7)
                throw new ArgumentOutOfRangeException(nameof(channel));

            string response = await SendCommand($"mx_{channel:X2}");
            return response.Length > 0 && response[0] == 6;
        }

        public async Task<bool> ConfigureRelays(bool owiClosed, bool maClosed, bool voClosed)
        {
            byte config = 0;
            if (owiClosed) config |= 0x40;
            if (maClosed) config |= 0x20;
            if (voClosed) config |= 0x10;

            string response = await SendCommand($"cfg{config:X2}");
            return response.Length > 0 && response[0] == 6;
        }

        public async Task<bool> ConnectOWI(int channel)
        {
            bool channelOk = await SelectChannel(channel);
            if (!channelOk) return false;

            await Task.Delay(10);

            bool allLow = await ConfigureRelays(false, false, false);
            if (!allLow) return false;

            await Task.Delay(10);

            return await ConfigureRelays(true, false, false);
        }

        public async Task<bool> DisconnectAll() => await ConfigureRelays(false, false, false);
    }
}
