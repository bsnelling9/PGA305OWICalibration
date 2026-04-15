using PGA305OWICalibration.Instruments;
using PGA305OWICalibration.PGA305;
using PGA305OWICalibration.PGA305EVM;
using System.Diagnostics;
using System.IO.Ports;
using TI.eLAB.EVM;

namespace PGA305OWICalibration
{
    public partial class Form1 : Form
    {
        private USB2AnyDevice _u2a = new USB2AnyDevice();
        private STM32Controller _stm32 = new STM32Controller();
        private PGA305Owi _pga305OWI = null!;
        private PGA305Device _pga305 = null!;
        private string _selectedPort = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Log(string message) => listBox1.Items.Add(message);

        //For production PCB
        private void btnGetHW_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            try
            {
                string[] ports = SerialPort.GetPortNames();
                if (ports.Length > 0)
                {
                    comboBox1.Items.AddRange(ports);
                    comboBox1.SelectedIndex = 0;
                    listBox1.Items.Add($"Found {ports.Length} COM ports.");
                }
                else
                {
                    listBox1.Items.Add("No COM ports detected.");
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add($"Port Scan Error: {ex.Message}");
            }
        }

        private async void btnConnectSTM32_Click(object sender, EventArgs e)
        {
            bool stm32Connected = _stm32.Open(_selectedPort);
            if (!stm32Connected)
            {
                listBox1.Items.Add($"Failed to open STM32 on {_selectedPort}.");
                return;
            }
            string identity = await _stm32.GetIdentity();
            listBox1.Items.Add($"STM32 identity: {identity}");
        }

        //For production PCB
        private void btnInitUSB2ANY_Click(object sender, EventArgs e)
        {
            try
            {
                _u2a.EnableDebugLogging();

                int numFound = _u2a.FindControllers();
                listBox1.Items.Add($"USB2ANY devices found: {numFound}");

                if (numFound == 0)
                {
                    listBox1.Items.Add("No USB2ANY detected.");
                    return;
                }

                string serial = _u2a.GetSerialNumber(0);
                listBox1.Items.Add($"USB2ANY serial: {serial}");

                bool opened = _u2a.Open("");
                if (!opened)
                {
                    listBox1.Items.Add("Failed to open USB2ANY.");
                    return;
                }

                listBox1.Items.Add($"USB2ANY opened. Handle = {_u2a.GetHandle()}");
            }
            catch (Exception ex)
            {
                listBox1.Items.Add($"Error: {ex.Message}");
            }
        }

        //For production PCB
        private async void btnFindDUT_Click(object sender, EventArgs e)
        {
            try
            {
                bool channelOk = await _stm32.SelectChannel(0);
                Log($"STM32 channel 0 select: {(channelOk ? "OK" : "FAILED")}");
                if (!channelOk) return;

                await Task.Delay(10);

                bool relayOk = await _stm32.ConfigureRelays(owiClosed: true, maClosed: false, voClosed: false);
                Log($"OWI relay closed: {(relayOk ? "OK" : "FAILED")}");
                if (!relayOk) return;

                await Task.Delay(10);

                bool owiConnected = await _stm32.ConnectOWI(0);
            }
            catch (Exception ex)
            {
                Log($"FindDUT error: {ex.Message}");
            }
        }

        //For production PCB
        private async void btnInitPGA305_Click(object sender, EventArgs e)
        {
            try
            {
                _pga305 = new PGA305Device(_u2a);

                bool initOk = _pga305.Initialize();

                listBox1.Items.Add($"PGA305 GPIO OWI init: {(initOk ? "OK" : "FAILED")}");

                if (initOk)
                    listBox1.Items.Add("Ready — click Read.");

                bool activated = await _pga305.Activate();
                Log($"PGA305 activate: {(activated ? "OK" : "FAILED")}");
                if (!activated) return;

                await Task.Delay(10);

                await _pga305.LoadEepromCache(0x00);

                string partNumber = await _pga305.ReadPartNumber();
                string serialNumber = await _pga305.ReadSerialNumber();

                Log($"Part number:   {partNumber}");
                Log($"Serial number: {serialNumber}");

            }
            catch (Exception ex)
            {
                listBox1.Items.Add($"Error: {ex.Message}");
            }
        }

        //For EVM
        private void u2a_Click(object sender, EventArgs e)
        {
            try
            {
                //_u2a.EnableDebugLogging();

                int numFound = _u2a.FindControllers();
                listBox1.Items.Add($"USB2ANY devices found: {numFound}");

                if (numFound == 0)
                {
                    listBox1.Items.Add("No USB2ANY detected.");
                    return;
                }

                string serial = _u2a.GetSerialNumber(0);
                listBox1.Items.Add($"USB2ANY serial: {serial}");

                bool opened = _u2a.Open("");
                if (!opened)
                {
                    listBox1.Items.Add("Failed to open USB2ANY.");
                    return;
                }

                listBox1.Items.Add($"USB2ANY opened. Handle = {_u2a.GetHandle()}");

                _pga305OWI = new PGA305Owi(_u2a);

                bool initOk = _pga305OWI.Initialize();
                listBox1.Items.Add($"PGA305 GPIO OWI init: {(initOk ? "OK" : "FAILED")}");

                if (initOk)
                    listBox1.Items.Add("Ready — click Read.");
            }
            catch (Exception ex)
            {
                listBox1.Items.Add($"Error: {ex.Message}");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (_pga305OWI == null)
            {
                listBox1.Items.Add("Please click U2A first.");
                return;
            }
            bool activated = _pga305OWI.Activate();
            listBox1.Items.Add($"PGA305 activation: {(activated ? "OK" : "FAILED")}");
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            _pga305OWI.Activate();
        }

        private void btnLoadEEPROM_Click(object sender, EventArgs e)
        {
            _pga305OWI.LoadEepromCache(0x0C);
        }

        private void btnPartSerial_Click(object sender, EventArgs e)
        {
            //string partNumber =  _pga305OWI.ReadPartNumber();
            string serialNumber =  _pga305OWI.ReadSerialNumber();

           // listBox1.Items.Add($"Part number:   {partNumber}");
            listBox1.Items.Add($"Serial number: {serialNumber}");
        }

      /*  private async void btnPartSerial_Click(object sender, EventArgs e)
        {
            string partNumber = await _pga305OWI.ReadPartNumberAsync();
            string serialNumber = await _pga305OWI.ReadSerialNumberAsync();

            listBox1.Items.Add($"Part number:   {partNumber}");
            listBox1.Items.Add($"Serial number: {serialNumber}");
        }*/

        private async void btnRead_Click(object sender, EventArgs e)
        {
            if (_pga305OWI == null)
            {
                listBox1.Items.Add("Please click U2A first.");
                return;
            }

            try
            {
                bool activated = _pga305OWI.Activate();
                listBox1.Items.Add($"PGA305 activation: {(activated ? "OK" : "FAILED")}");
                await Task.Delay(2);

                if (!activated)
                {
                    string errorText = "";
                    _u2a.GetStatusText(-1, ref errorText);
                    listBox1.Items.Add($"Activation error: {errorText}");
                    return;
                }

                //await _pga305OWI.LoadEepromCache(0x00);

                string partNumber = await _pga305OWI.ReadPartNumberAsync();
                string serialNumber = await _pga305OWI.ReadSerialNumberAsync();

                listBox1.Items.Add($"Part number:   {partNumber}");
                listBox1.Items.Add($"Serial number: {serialNumber}");
            }
            catch (Exception ex)
            {
                listBox1.Items.Add($"Error: {ex.Message}");
            }
        }

        private void btnDebugGPIO_Click(object sender, EventArgs e)
        {
            try
            {
                _u2a.GPIO_SetPort(7, 1);

                listBox1.Items.Add("GPIO7 → HIGH. Check TP51 with multimeter...");
                _u2a.GPIO_WritePort(7, 1);
                Thread.Sleep(2000);

                listBox1.Items.Add("GPIO7 → LOW. Check TP51 with multimeter...");
                _u2a.GPIO_WritePort(7, 0);
                Thread.Sleep(2000);

                listBox1.Items.Add("GPIO7 debug complete.");
            }
            catch (Exception ex)
            {
                listBox1.Items.Add($"GPIO debug error: {ex.Message}");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                _selectedPort = comboBox1.SelectedItem.ToString();
                listBox1.Items.Add($"Target Port: {_selectedPort}");
            }
        }

        private void btnTestGPIO11_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Add("--- GPIO11 Force Test ---");

                if (_u2a.GetHandle() <= 0)
                {
                    listBox1.Items.Add("Device not open — click U2A first");
                    return;
                }

                int r1 = _u2a.GPIO_SetPort(11, 2);
                int r2 = _u2a.GPIO_WritePort(11, 2);
                listBox1.Items.Add($"GPIO11 HIGH — SetPort:{r1} WritePort:{r2} — measure TP60 now");
                Thread.Sleep(3000);

                int r3 = _u2a.GPIO_WritePort(11, 1);
                listBox1.Items.Add($"GPIO11 LOW — WritePort:{r3} — measure TP60 now");
                Thread.Sleep(3000);

                int r4 = _u2a.GPIO_WritePort(11, 2);
                listBox1.Items.Add($"GPIO11 HIGH again — WritePort:{r4}");
                Thread.Sleep(3000);

                listBox1.Items.Add("--- Did TP60 follow GPIO11? ---");
            }
            catch (Exception ex)
            {
                listBox1.Items.Add($"Error: {ex.Message}");
            }
        }

        private void btnManualHardwareTest_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Add("--- Manual Hardware Test ---");

                if (_u2a.GetHandle() <= 0)
                {
                    listBox1.Items.Add("Device not open — click U2A first");
                    return;
                }

                int r = _u2a.Power_WriteControl(Power_3V3.ON, Power_5V0.ON);
                listBox1.Items.Add($"Power ON result: {r}");
                Thread.Sleep(50);

                int r1 = _u2a.GPIO_SetPort(11, 1);
                int r2 = _u2a.GPIO_WritePort(11, 1);
                listBox1.Items.Add($"GPIO11 HIGH — SetPort:{r1} WritePort:{r2}");
                Thread.Sleep(10);

                int r3 = _u2a.DACs_Write(DACs_WhichDAC.DAC0, DACs_OperatingMode.Normal, 0);
                listBox1.Items.Add($"DAC0 = 0 result: {r3}");
                Thread.Sleep(10);

                int r4 = _u2a.GPIO_SetPort(10, 1);
                int r5 = _u2a.GPIO_WritePort(10, 1);
                listBox1.Items.Add($"GPIO10 HIGH — SetPort:{r4} WritePort:{r5} — measure TP20 now");
                Thread.Sleep(3000);

                int r6 = _u2a.GPIO_WritePort(10, 0);
                listBox1.Items.Add($"GPIO10 LOW — WritePort:{r6} — measure TP20 now");
                Thread.Sleep(3000);

                listBox1.Items.Add("--- Did TP20 change between HIGH and LOW? ---");

                int r7 = _u2a.GPIO_SetPort(7, 1);
                int r8 = _u2a.GPIO_WritePort(7, 1);
                listBox1.Items.Add($"GPIO7 HIGH — SetPort:{r7} WritePort:{r8} — measure TP4");
                Thread.Sleep(3000);

                int r9 = _u2a.GPIO_WritePort(7, 0);
                listBox1.Items.Add($"GPIO7 LOW — WritePort:{r9}");
                Thread.Sleep(3000);

                int r10 = _u2a.GPIO_WritePort(7, 1);
                listBox1.Items.Add($"GPIO7 HIGH again — WritePort:{r10}");
                Thread.Sleep(3000);

                _u2a.GPIO_WritePort(7, 0);
                listBox1.Items.Add("--- Test complete ---");
            }
            catch (Exception ex)
            {
                listBox1.Items.Add($"Error: {ex.Message}");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                if (_u2a.GetHandle() > 0)
                {
                    _u2a.GPIO_WritePort(PGA305Owi.GPIO7, PGA305Owi.STATE_LOW);
                    _u2a.GPIO_WritePort(PGA305Owi.GPIO10, PGA305Owi.STATE_LOW);
                    _u2a.GPIO_WritePort(PGA305Owi.GPIO11, PGA305Owi.STATE_LOW);
                    _u2a.OneWire_SetOutput(0);
                    _u2a.OneWire_SetMode(0);
                    Thread.Sleep(10);
                }
            }
            catch { }

            _stm32.Close();
            _u2a.Close();
            base.OnFormClosing(e);
        }
    }
}