using System;
using System.IO.Ports; // This requires the NuGet package "System.IO.Ports"
using System.Threading;

namespace PGA305OWICalibration.Instruments
{
    public class STM32Controller
    {
        private SerialPort? _serialPort;


        private const byte RELAY_OWI = 0x40;  
        private const byte RELAY_MA = 0x20;  
        private const byte RELAY_VO = 0x10;  
        private const byte VCOMP1_HIGH = 0x02;  
        private const byte VCOMP0_HIGH = 0x01;

        public bool IsConnected => _serialPort?.IsOpen ?? false;

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

        public async Task<string> RawTest()
        {
            if (_serialPort == null) return "port is null";

            _serialPort.DiscardInBuffer();
            _serialPort.DiscardOutBuffer();
                        
            byte[] toSend = new byte[] { 0x49, 0x44, 0x4E, 0x0A }; // I D N \n
            _serialPort.Write(toSend, 0, toSend.Length);

            System.Diagnostics.Debug.WriteLine(
                $"Sent bytes: {BitConverter.ToString(toSend)}");

            await Task.Delay(200);

            int waiting = _serialPort.BytesToRead;
            System.Diagnostics.Debug.WriteLine($"Bytes in RX buffer: {waiting}");

            if (waiting == 0) return "nothing received";

            byte[] buf = new byte[waiting];
            _serialPort.Read(buf, 0, waiting);

            System.Diagnostics.Debug.WriteLine(
                $"Received hex: {BitConverter.ToString(buf)}");
            System.Diagnostics.Debug.WriteLine(
                $"Received txt: {System.Text.Encoding.ASCII.GetString(buf)}");

            return BitConverter.ToString(buf);
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

        public async Task<bool> ConfigureRelays(bool owiClosed, bool maClosed, bool voClosed, bool vcomp0High, bool vcomp1High)
        {
            byte config = 0;

            if (owiClosed) config |= RELAY_OWI;
            if (maClosed) config |= RELAY_MA;
            if (voClosed) config |= RELAY_VO;
            if (vcomp1High) config |= VCOMP1_HIGH;
            if (vcomp0High) config |= VCOMP0_HIGH;

            string response = await SendCommand($"cfg{config:X2}");
            return response.Length > 0 && response[0] == 6;
        }

        public async Task<bool> ConnectOWI(int channel)
        {
            bool channelOk = await SelectChannel(channel);
            if (!channelOk) return false;

            await Task.Delay(10);

            bool allLow = await ConfigureRelays(
                owiClosed: false, maClosed: false, voClosed: false,
                vcomp0High: false, vcomp1High: false);
            if (!allLow) return false;

            await Task.Delay(10);

            return await ConfigureRelays(
                owiClosed: true, maClosed: false, voClosed: true,
                vcomp0High: true, vcomp1High: true);
        }

        public async Task<bool> DisconnectAll()
        {
            return await ConfigureRelays(
                owiClosed: false, maClosed: false, voClosed: false,
                vcomp0High: false, vcomp1High: false);
        }
    }
}
