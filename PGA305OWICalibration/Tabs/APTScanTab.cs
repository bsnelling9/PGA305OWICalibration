using PGA305OWICalibration.Instruments;
using PGA305OWICalibration.PGA305;
using PGA305OWICalibration.UIControls;

using System.Diagnostics;

namespace PGA305OWICalibration.Tabs
{
    public partial class APTScanTab : UserControl
    {
        private ATPButton[] _channelButtons = new ATPButton[8];
        private ATPButton _btnScanAll;
        private STM32Controller _stm32;
        private PGA305Device _pga305;

        public APTScanTab(STM32Controller stm32, PGA305Device pga305)
        {
            InitializeComponent();
            _stm32 = stm32;
            _pga305 = pga305;
            CreateButtons();
            this.VisibleChanged += APTScanTab_VisibleChanged;
        }

        private void APTScanTab_VisibleChanged(object sender, EventArgs e)
        {
            if (!Visible) return;

            if (_stm32.IsConnected)
                modeButton.SetMode(STM32Mode.OWI);
            else
                modeButton.SetMode(STM32Mode.NotConnected);
        }

        private void CreateButtons()
        {
            for (int i = 0; i < 8; i++)
            {
                _channelButtons[i] = CreateStyledButton(
                    $"Channel {i + 1}",
                    $"btnChannel{i + 1}",
                    new Point(200, 20 + (i * 50))
                );
                _channelButtons[i].Click += ChannelButton_Click;
                this.Controls.Add(_channelButtons[i]);
            }

            _btnScanAll = CreateStyledButton("Scan All", "btnScanAll", new Point(200, 20 + (8 * 50)));
            _btnScanAll.Click += BtnScanAll_Click;
            this.Controls.Add(_btnScanAll);
        }

        private ATPButton CreateStyledButton(string text, string name, Point location)
        {
            ATPButton btn = new ATPButton
            {
                Text = text,
                Name = name,
                Width = 160,
                Height = 45,
                Location = location,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 10),
                Cursor = Cursors.Hand,
                BorderColor = Color.Black,
                CornerRadius = 10,
                BorderSize = 3,
                FlatAppearance = { BorderSize = 0 },
                TabStop = false,
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void SetButtonActive(ATPButton btn)
        {
            btn.BorderColor = Color.RoyalBlue;
            btn.Invalidate();
        }

        private void SetButtonSuccess(ATPButton btn)
        {
            btn.BorderColor = Color.Green;
            btn.Invalidate();
        }

        private void SetButtonFailure(ATPButton btn)
        {
            btn.BorderColor = Color.Red;
            btn.Invalidate();
        }

        private void ResetButton(ATPButton btn)
        {
            btn.BorderColor = Color.Black;
            btn.Invalidate();
        }

        private void DisableAllButtons()
        {
            foreach (var btn in _channelButtons)
            {
                btn.Enabled = false;
                btn.BackColor = Color.LightGray;
            }
            _btnScanAll.Enabled = false;
            _btnScanAll.BackColor = Color.LightGray;
        }

        private void EnableAllButtons()
        {
            foreach (var btn in _channelButtons)
            {
                btn.Enabled = true;
                btn.BackColor = Color.White;
            }
            _btnScanAll.Enabled = true;
            _btnScanAll.BackColor = Color.White;
        }

        private async void ChannelButton_Click(object sender, EventArgs e)
        {
            ATPButton clicked = (ATPButton)sender;
            int channel = int.Parse(clicked.Name.Replace("btnChannel", ""));

            DisableAllButtons();
            SetButtonActive(clicked);
            try
            {
                bool success = ReadChannel(channel);
                if (success)
                    SetButtonSuccess(clicked);
                else
                    SetButtonFailure(clicked);
            }
            finally
            {
                EnableAllButtons();
            }
        }

        private async void BtnScanAll_Click(object sender, EventArgs e)
        {
            DisableAllButtons();
            try
            {
                for (int i = 1; i <= 8; i++)
                    ReadChannel(i);
            }
            finally
            {
                EnableAllButtons();
            }
        }

        private bool ReadChannel(int channel)
        {
            try
            {
                int stm32Channel = channel - 1;

                bool channelOk = _stm32.SelectChannel(stm32Channel);
                Debug.WriteLine($"STM32 IsConnected: {_stm32.IsConnected}");

                Debug.WriteLine($"STM32 current cfg: 0x{_stm32.CurrentConfig:X2}");
                if (!channelOk)
                {
                    Debug.WriteLine($"Channel {channel}: STM32 select failed");
                    return false;
                }

                bool init = _pga305.Initialize();
                if (!init)
                {
                    Debug.WriteLine($"Channel {channel}: PGA305 init failed");
                    return false;
                }

                bool activated = _pga305.Activate();
                if (!activated)
                {
                    Debug.WriteLine($"Channel {channel}: PGA305 activate failed");
                    return false;
                }

                // Read data
                //string partNumber = _pga305.ReadPartNumber();
                //string serialNumber = _pga305.ReadSerialNumber();

               // Debug.WriteLine($"Channel {channel}: PN={partNumber} SN={serialNumber}");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Channel {channel} error: {ex.Message}");
                return false;
            }
        }
    }
}
