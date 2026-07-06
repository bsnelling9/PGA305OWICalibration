using PGA305OWICalibration.Instruments;
using System.Diagnostics;
using System.IO.Ports;


// To DO
// Remove all voltage selection from this tab
// This is for the hardware only
// The output should be moved the the ATP TAB

namespace PGA305OWICalibration.Tabs
{
    public partial class HardwareTab : UserControl
    {
        private STM32Controller _stm32;
        private USB2AnyDevice _u2a;

        public HardwareTab(STM32Controller stm32, USB2AnyDevice u2a)
        {
            InitializeComponent();
            _stm32 = stm32;
            _u2a = u2a;
            lblVoltageRange.Visible = false;
            lstVoltageRange.Visible = false;
        }

        private void BtnGetPorts_Click(object sender, EventArgs e)
        {
            cmbPorts.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length > 0)
            {
                cmbPorts.Items.AddRange(ports);
                cmbPorts.SelectedIndex = 0;
            }
        }

        private void BtnConnectSTM32_Click(object sender, EventArgs e)
        {
            try
            {
                _stm32.Close();
                string port = cmbPorts.SelectedItem?.ToString() ?? "";
                bool connected = _stm32.Open(port);

                if (!connected)
                {
                    lblSTM32Status.Text = "STM32: Failed";
                    lblSTM32Status.ForeColor = Color.Red;
                    return;
                }

                string identity = _stm32.GetIdentity();
                lblSTM32Status.Text = $"STM32: {identity}";
                lblSTM32Status.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblSTM32Status.Text = $"STM32: Error - {ex.Message}";
                lblSTM32Status.ForeColor = Color.Red;
            }
        }

        private void BtnConnectUSB2ANY_Click(object sender, EventArgs e)
        {
            try
            {
                int numFound = _u2a.FindControllers();
                if (numFound == 0)
                {
                    lblUSB2ANYStatus.Text = "USB2ANY: Not Found";
                    lblUSB2ANYStatus.ForeColor = Color.Red;
                    return;
                }

                bool opened = _u2a.Open("");
                lblUSB2ANYStatus.Text = opened ? $"USB2ANY: Connected (Handle={_u2a.GetHandle()})" : "USB2ANY: Failed";
                lblUSB2ANYStatus.ForeColor = opened ? Color.Green : Color.Red;
            }
            catch (Exception ex)
            {
                lblUSB2ANYStatus.Text = $"USB2ANY: Error - {ex.Message}";
                lblUSB2ANYStatus.ForeColor = Color.Red;
            }
        }

        private void BtnSetCompensation_Click(object sender, EventArgs e)
        {
            try
            {
                bool ok = _stm32.ConfigureVoltageComparators(
                    vcompa0High: chkVCOMPA0.Checked,
                    vcompa1High: chkVCOMPA1.Checked);

                lblCompensationStatus.Text = ok ? "Compensation Set" : "Failed";
                lblCompensationStatus.ForeColor = ok ? Color.Green : Color.Red;
            }
            catch (Exception ex)
            {
                lblCompensationStatus.Text = $"Error: {ex.Message}";
                lblCompensationStatus.ForeColor = Color.Red;
            }
        }

        private void BtnSetRelay_Click(object sender, EventArgs e)
        {
            try
            {
                bool ok = _stm32.ConfigureRelays(
                    owiRelayClosed: rdoOWI.Checked,
                    maRelayClosed: rdoMA.Checked,
                    voRelayClosed: rdoOWI.Checked || rdoVO.Checked);

                string mode = rdoOWI.Checked ? "OWI" : rdoVO.Checked ? "VO" : "MA";
                lblRelayStatus.Text = ok ? $"{mode} Relay Set" : "Failed";
                lblRelayStatus.ForeColor = ok ? Color.Green : Color.Red;
            }
            catch (Exception ex)
            {
                lblRelayStatus.Text = $"Error: {ex.Message}";
                lblRelayStatus.ForeColor = Color.Red;
            }
        }

        private void BtnScanHardware_Click(object sender, EventArgs e)
        {
            dgvHardware.Rows.Clear();
            try
            {
                string[] ports = SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    dgvHardware.Rows.Add(port, port, "Found");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"COM scan error: {ex.Message}");
            }

            try
            {
                int numFound = _u2a.FindControllers();
                if (numFound > 0)
                {
                    string serial = _u2a.GetSerialNumber(0);
                    dgvHardware.Rows.Add("USB2ANY", serial, "Found");
                }
                else
                {
                    dgvHardware.Rows.Add("USB2ANY", "-", "Not Found");
                }
            }
            catch (Exception ex)
            {
                dgvHardware.Rows.Add("USB2ANY", "-", $"Error: {ex.Message}");
            }
        }

        private void BtnConnectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvHardware.Rows)
            {
                string port = row.Cells[1].Value?.ToString() ?? "";

                if (!port.StartsWith("COM")) continue;

                _stm32.Close();
                bool connected = _stm32.Open(port);
                if (!connected)
                {
                    row.Cells[2].Value = "Failed to Open";
                    continue;
                }

                string identity = _stm32.GetIdentity();
                if (identity.Contains("PGA305"))
                {
                    row.Cells[0].Value = identity;
                    row.Cells[2].Value = "Connected";
                    lblSTM32Status.Text = $"STM32: {identity}";
                    lblSTM32Status.ForeColor = Color.Green;
                    break;
                }
                else
                {
                    _stm32.Close();
                    row.Cells[0].Value = identity.Length > 0 ? identity : "Unknown";
                    row.Cells[2].Value = "Not STM32";
                }
            }

            try
            {
                bool opened = _u2a.Open("");
                lblUSB2ANYStatus.Text = opened ? $"USB2ANY: Connected (Handle={_u2a.GetHandle()})" : "USB2ANY: Failed";
                lblUSB2ANYStatus.ForeColor = opened ? Color.Green : Color.Red;

                foreach (DataGridViewRow row in dgvHardware.Rows)
                {
                    if (row.Cells[0].Value?.ToString() == "USB2ANY")
                    {
                        row.Cells[0].Value = opened ? $"USB2ANY ({_u2a.GetSerialNumber(0)})" : "USB2ANY";
                        row.Cells[2].Value = opened ? "Connected" : "Failed";
                    }
                }
            }
            catch (Exception ex)
            {
                lblUSB2ANYStatus.Text = $"USB2ANY: Error - {ex.Message}";
                lblUSB2ANYStatus.ForeColor = Color.Red;
            }
        }

        private void BtnOutputV_Click(object sender, EventArgs e)
        {
            lblVoltageRange.Visible = true;
            lstVoltageRange.Visible = true;
        }

        private void BtnOutputRM_Click(object sender, EventArgs e)
        {
        }

        private void BtnOutputC_Click(object sender, EventArgs e)
        {
        }

        private void LstVoltageRange_Click(object sender, EventArgs e)
        {
            if (lstVoltageRange.SelectedItem != null)
                Debug.WriteLine($"Voltage range selected: {lstVoltageRange.SelectedItem}");
        }
    }
}