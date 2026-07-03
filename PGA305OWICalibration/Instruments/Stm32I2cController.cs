using System.Diagnostics;
using System.IO.Ports;


namespace PGA305OWICalibration.Instruments
{
    internal class Stm32I2cController
    {
        private SerialPort? _serialPort;

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

        public bool EnterCommandMode(int i2cAddr, int maxRetries = 3)
        {
            for (int attempt = 0; attempt < maxRetries; attempt++)
            {
                byte[] response = SendCommandRaw("cm_20");
                Debug.WriteLine($"cm_20 attempt {attempt}: [{BitConverter.ToString(response)}]");

                if (response.Length >= 1 && response[0] == 0x06)
                {
                    int? reg01 = ReadRegister(0x01, i2cAddr);
                    Debug.WriteLine($"Register 0x01 = {reg01?.ToString("X2") ?? "null"}");

                    if (reg01 == 0x03)
                        return true;
                }

                if (attempt < maxRetries - 1)
                    ResetI2C();
            }
            return false;
        }

        private byte[] SendCommandRaw(string command)
        {
            if (!IsConnected || _serialPort == null)
                throw new InvalidOperationException("STM32 I2C is not connected.");

            _serialPort.Write(command + "\n");

            try
            {
                string response = _serialPort.ReadTo("\n"); 
                return System.Text.Encoding.ASCII.GetBytes(response);
            }
            catch (TimeoutException)
            {
                Debug.WriteLine($"STM32I2C >> {command} << TIMEOUT");
                return Array.Empty<byte>();
            }
        }

        public string GetIdentity()
        {
            byte[] raw = SendCommandRaw("IDN");
            return System.Text.Encoding.ASCII.GetString(raw).Trim();
        }

        public int? ReadRegister(int regAddr, int i2cAddr)
        {
            byte[] raw = SendCommandRaw($"imr{i2cAddr:X2}{regAddr:X2}");
            Debug.WriteLine($"ReadRegister raw: [{BitConverter.ToString(raw)}]");
            if (raw.Length >= 2)
            {
                try
                {
                    string hex = System.Text.Encoding.ASCII.GetString(raw, 0, 2);
                    return Convert.ToInt32(hex, 16);
                }
                catch { return null; }
            }
            return null;
        }

        public void SetChannel(int channel)
        {
            SendCommandRaw($"mx2{channel:X2}");
            Thread.Sleep(100); 
        }

        public void DisconnectChannel()
        {
            SendCommandRaw("mx001");
            Thread.Sleep(100);
        }

        public void ResetI2C()
        {
            SendCommandRaw("i2cr");
            Thread.Sleep(100); 
        }
    }
}
