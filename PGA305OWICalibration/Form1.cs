using PGA305OWICalibration.Instruments;
using PGA305OWICalibration.PGA305;
using PGA305OWICalibration.PGA305EVM;
using PGA305OWICalibration.Tabs;
using System.Diagnostics;
using System.IO.Ports;

namespace PGA305OWICalibration
{
    public partial class Form1 : Form
    {
        private USB2AnyDevice _u2a = new USB2AnyDevice();
        private STM32Controller _stm32 = new STM32Controller();
        private PGA305Device _pga305 = null!;
        private string _selectedPort = "";

        public Form1()
        {
            InitializeComponent();

            _pga305 = new PGA305Device(_u2a);

            HardwareTab hardwareTab = new HardwareTab(_stm32, _u2a);
            hardwareTab.Dock = DockStyle.Fill;
            this.hardwareTab.Controls.Add(hardwareTab);

            APTScanTab aptScanTab = new APTScanTab(_stm32, _pga305);
            aptScanTab.Dock = DockStyle.Fill;
            this.deviceTab.Controls.Add(aptScanTab);
        }

        private void Log(string message) => listBox1.Items.Add(message);

        private void btnDebug_Click(object sender, EventArgs e)
        {
            Form2 debugForm = new Form2();
            debugForm.Show();
        }

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

        private void btnConnectSTM32_Click(object sender, EventArgs e)
        {
            try { _stm32.Close(); } catch { }
            _stm32 = new STM32Controller();

            _selectedPort = comboBox1.SelectedItem?.ToString() ?? "";
            Log($"Attempting open on: '{_selectedPort}'");

            bool stm32Connected = _stm32.Open(_selectedPort);

            Debug.WriteLine($"Port open result: {stm32Connected}");

            if (!stm32Connected)
            {
                Log("Failed to open port — is another app using it?");
                return;
            }

            string identity = _stm32.GetIdentity();
            bool vcompOk = _stm32.ConfigureVoltageComparators(vcompa0High: true, vcompa1High: true);
            Debug.WriteLine($"VCOMP set: {vcompOk}");

            bool relayOk = _stm32.ConfigureRelays(owiRelayClosed: true, maRelayClosed: false, voRelayClosed: true);
            Debug.WriteLine($"Relay set: {relayOk}");
            Debug.WriteLine($"STM32 identity: '{identity}'");

            Log($"STM32 identity: '{identity}'");
        }

        private async void btnFindDUT_Click(object sender, EventArgs e)
        {
            try
            {
                bool connected = _stm32.ConnectOWI(0);
                Log($"OWI connect channel 0: {(connected ? "OK" : "FAILED")}");
            }
            catch (Exception ex)
            {
                Log($"FindDUT error: {ex.Message}");
            }
        }

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

        private void btnInitPGA305_Click(object sender, EventArgs e)
        {
            try
            {
                _pga305 = new PGA305Device(_u2a);

                bool initOk = _pga305.Initialize();

                listBox1.Items.Add($"PGA305 GPIO OWI init: {(initOk ? "OK" : "FAILED")}");

                if (initOk)
                    listBox1.Items.Add("Ready — click Read.");
            }
            catch (Exception ex)
            {
                listBox1.Items.Add($"Error: {ex.Message}");
            }
        }

        private void btnActPGA_Click(object sender, EventArgs e)
        {
            try
            {
                bool activated = _pga305.Activate();
                Log($"PGA305 activate: {(activated ? "OK" : "FAILED")}");
                if (!activated) return;

            }
            catch (Exception ex)
            {
                listBox1.Items.Add($"Error: {ex.Message}");
            }
        }

        private void btnGetMetaData_Click(object sender, EventArgs e)
        {
            try
            {
                string partNumber = _pga305.ReadPartNumber();
                string serialNumber = _pga305.ReadSerialNumber();

                Log($"Part number:   {partNumber}");
                //Log($"Serial number: {serialNumber}");

            }
            catch (Exception ex)
            {
                listBox1.Items.Add($"Error: {ex.Message}");
            }
        }
        private void btnGetDIG_IF_CNTRL_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                _selectedPort = comboBox1.SelectedItem.ToString();
                listBox1.Items.Add($"Target Port: {_selectedPort}");
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
                    _u2a.GPIO_WritePort(PGA305Device.GPIO7, PGA305Device.STATE_LOW);
                    _u2a.GPIO_WritePort(PGA305Device.GPIO10, PGA305Device.STATE_LOW);
                    _u2a.GPIO_WritePort(PGA305Device.GPIO11, PGA305Device.STATE_LOW);
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}