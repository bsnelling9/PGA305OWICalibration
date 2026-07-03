using PGA305OWICalibration.Config;

namespace PGA305OWICalibration
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            txtAPIURL.Text = AppConfig.API_URL;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string newUrl = txtAPIURL.Text.Trim();

            if (string.IsNullOrEmpty(newUrl))
            {
                MessageBox.Show("API URL cannot be empty.", "Invalid URL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AppConfig.SaveApiUrl(newUrl);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtAPIURL_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = Uri.TryCreate(txtAPIURL.Text.Trim(), UriKind.Absolute, out _);
        }
    }
}