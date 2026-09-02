using PGA305OWICalibration.Config;

namespace PGA305OWICalibration
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            txtAPIURL.Text = AppConfig.API_URL;
            txtMuxPort.Text = AppConfig.STM32Port;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string newUrl = txtAPIURL.Text.Trim();
            string newPort = txtMuxPort.Text.Trim().ToUpperInvariant();

            if (newUrl.Length == 0 || newPort.Length == 0)
            {
                MessageBox.Show("API URL and COM port cannot be empty.", "Invalid settings",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AppConfig.SaveApiUrl(newUrl);
            AppConfig.SaveMuxPort("STM32COMPORT", newPort);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void UpdateSaveButton()
        {
            bool urlOk = Uri.TryCreate(txtAPIURL.Text.Trim(), UriKind.Absolute, out _);
            bool portOk = txtMuxPort.Text.Trim().Length > 0;

            btnSave.Enabled = urlOk && portOk;
        }

        private void txtAPIURL_TextChanged(object sender, EventArgs e) => UpdateSaveButton();
        private void txtMuxPort_TextChanged(object sender, EventArgs e) => UpdateSaveButton();
    }
}