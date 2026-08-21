using PGA305OWICalibration.API;
using PGA305OWICalibration.Config;
using PGA305OWICalibration.Instruments;
using PGA305OWICalibration.PGA305;
using PGA305OWICalibration.UIControls;
using System.Diagnostics;

namespace PGA305OWICalibration.Tabs
{
    public partial class APTScanTab : UserControl
    {
        private const int ChannelCount = 8;

        private const int CompensationSettleMs = 10;
        private const int RelaySettleMs = 20;
        private const int ChannelSettleMs = 10;

        private readonly ApiClient _api = new ApiClient();
        private readonly StockCodes _stockCodes = new StockCodes();
        private readonly STM32Controller _stm32;
        private readonly PGA305Device _pga305;

        private readonly ChannelCard[] _cards = new ChannelCard[ChannelCount];

        private ChannelCard _activeCard;
        private string _jobCode = string.Empty;

        public APTScanTab(STM32Controller stm32, PGA305Device pga305)
        {
            InitializeComponent();
            _stm32 = stm32;
            _pga305 = pga305;
            CreateCards();
        }

        private void CreateCards()
        {
            for (int i = 0; i < ChannelCount; i++)
            {
                ChannelCard card = AppConfig.TestMode
                    ? new ManualCard()
                    : new StockCodeCard();

                card.Channel = i;
                card.Dock = DockStyle.Fill;
                card.Margin = new Padding(6);

                card.ConnectRequested += Card_ConnectRequested;
                card.ConfigureRequested += Card_ConfigureRequested;

                _cards[i] = card;
                tlpCards.Controls.Add(card, i % 4, i / 4);
            }
        }

        private bool TryClaimBus(ChannelCard card)
        {
            if (_activeCard != null && _activeCard != card)
                return false;

            _activeCard = card;

            foreach (var c in _cards)
                c.SetInteractive(c == card);

            return true;
        }

        private void ReleaseBus()
        {
            _activeCard = null;

            foreach (var c in _cards)
                c.SetInteractive(true);
        }

        private bool SetChannel(int channel)
        {
            if (!_stm32.SelectChannel(channel))
            {
                Debug.WriteLine($"Channel {channel}: STM32 select failed");
                return false;
            }

            Debug.WriteLine($"Channel {channel} selected, mux register 0x{_stm32.CurrentConfig:X2}");
            return true;
        }

        private bool ConfigureMuxForOwi(string signalType)
        {
            if (!AppConfig.Compensation.TryGetValue(signalType, out var comp))
            {
                Debug.WriteLine($"No compensation defined for '{signalType}'");
                return false;
            }

            if (!_stm32.ConfigureVoltageComparators(comp.VCompA0High, comp.VCompA1High))
            {
                Debug.WriteLine("Compensation failed");
                return false;
            }

            Thread.Sleep(CompensationSettleMs);

            if (!_stm32.ConfigureRelays(owiRelayClosed: true, maRelayClosed: false, voRelayClosed: true))
            {
                Debug.WriteLine("Relay config failed");
                return false;
            }

            Thread.Sleep(RelaySettleMs);
            return true;
        }

        private void Card_StockCodeEntered(object sender, EventArgs e)
        {
            var card = (StockCodeCard)sender;
            var config = card.OutputConfig;

            var spec = _stockCodes.Lookup(card.StockCodeText);

            if (spec == null)
            {
                card.BorderColor = Color.Red;
                card.ShowProgress($"Unknown stock code '{card.StockCodeText}'");
                return;
            }

            switch (spec.Output.Type.ToLowerInvariant())
            {
                case "ratiometric":
                    config.SelectRatiometric();
                    break;

                case "current":
                    config.SelectCurrent();
                    break;

                case "voltage":
                    config.SelectVoltage($"{spec.Output.Min:0.##}-{spec.Output.Max:0.##}V");
                    break;

                default:
                    card.BorderColor = Color.Red;
                    card.ShowProgress($"Unknown output type '{spec.Output.Type}'");
                    return;
            }

            config.StockCode = spec.StockCode;
            config.PressureUnit = spec.Pressure.Units;
            config.pMin = (int)spec.Pressure.Min;
            config.pMax = (int)spec.Pressure.Max;

            card.ClearProgress();
            card.BorderColor = Color.Gainsboro;
            card.UpdateDisplay();
        }

        private async void Card_ConnectRequested(object sender, EventArgs e)
        {
            var card = (ChannelCard)sender;

            if (card is StockCodeCard stockCard)
            {
                if (!LoadStockCode(stockCard))
                    return;

                if (card.DeviceConnected)
                {
                    card.UpdateDisplay();
                    return;
                }
            }

            if (!_stm32.IsConnected)
            {
                card.BorderColor = Color.Goldenrod;
                Debug.WriteLine("STM32 not connected, connect hardware first");
                return;
            }

            if (!TryClaimBus(card))
                return;

            card.BorderColor = Color.RoyalBlue;

            bool ok = await Task.Run(() => ConnectDevice(card.Channel, card.OutputConfig));

            if (!ok)
            {
                card.DeviceConnected = false;
                card.BorderColor = Color.Red;
                ReleaseBus();
                return;
            }

            card.DeviceConnected = true;
        }

        private bool LoadStockCode(StockCodeCard card)
        {
            var spec = _stockCodes.Lookup(card.StockCodeText);

            if (spec == null)
            {
                card.BorderColor = Color.Red;
                card.ShowMessage($"Stock code '{card.StockCodeText}' not found.{Environment.NewLine}Check it and try again.");
                return false;
            }

            var config = card.OutputConfig;

            switch (spec.Output.Type.ToLowerInvariant())
            {
                case "ratiometric":
                    config.SelectRatiometric();
                    break;

                case "current":
                    config.SelectCurrent();
                    break;

                case "voltage":
                    config.SelectVoltage($"{spec.Output.Min:0.##}-{spec.Output.Max:0.##}V");
                    break;

                default:
                    card.BorderColor = Color.Red;
                    card.ShowMessage($"Stock code '{spec.StockCode}' has unknown output type '{spec.Output.Type}'.");
                    return false;
            }

            config.StockCode = spec.StockCode;
            config.PressureUnit = spec.Pressure.Units;
            config.pMin = (int)spec.Pressure.Min;
            config.pMax = (int)spec.Pressure.Max;

            card.ShowMessage(string.Empty);
            return true;
        }

        private bool ConnectDevice(int channel, PGAOutputConfig config)
        {
            try
            {
                if (!ConfigureMuxForOwi(config.SignalType))
                    return false;

                if (!SetChannel(channel))
                    return false;

                Thread.Sleep(ChannelSettleMs);

                if (!_pga305.Initialize())
                {
                    Debug.WriteLine($"Channel {channel}: PGA305 init failed");
                    return false;
                }

                if (!_pga305.Activate())
                {
                    Debug.WriteLine($"Channel {channel}: PGA305 activate failed");
                    return false;
                }

                config.SerialNumber = _pga305.ReadInternalSerialNumber();

                if (config.SerialNumber <= 0)
                {
                    Debug.WriteLine($"Channel {channel}: bad internal serial number {config.SerialNumber}");
                    return false;
                }

                config.PressureCode = _pga305.ReadPressureCode();

                if (string.IsNullOrWhiteSpace(config.PressureCode))
                {
                    Debug.WriteLine($"Channel {channel}: no pressure code");
                    return false;
                }

                config.SensorSerialNumber = _pga305.ReadSerialNumber();
                config.SetPressureRangeFromCode();

                Debug.WriteLine($"Channel {channel}: SN={config.SerialNumber} sensor={config.SensorSerialNumber} code={config.PressureCode}");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Channel {channel} connect error: {ex.Message}");
                return false;
            }
        }

        private async void Card_ConfigureRequested(object sender, EventArgs e)
        {
            var card = (ChannelCard)sender;

            if (_activeCard != card)
                return;

            var config = card.OutputConfig;
            config.JobCode = _jobCode;

            if (!PressureRangeIsLegal(config))
            {
                card.BorderColor = Color.Red;
                card.ShowProgress($"{config.pMax} {config.PressureUnit} exceeds pressure code {config.PressureCode}");
                return;
            }

            card.SetInteractive(false);
            card.BorderColor = Color.RoyalBlue;
            card.ClearProgress();
            card.ShowProgress("Calculating coefficients...");

            try
            {
                var result = await _api.ConvertOutput(
                    config.SerialNumber, config.vMin, config.vMax,
                    config.pMin, config.pMax, config.PressureUnit);

                if (result == null)
                {
                    card.ShowProgress("Coefficient calculation FAILED");
                    Fail(card, "convert-output failed");
                    return;
                }

                if (result.serial_number != config.SerialNumber)
                {
                    card.ShowProgress($"Serial mismatch: expected {config.SerialNumber}, got {result.serial_number}");
                    Fail(card, "serial number mismatch");
                    return;
                }

                card.ShowProgress("Coefficients received");
                card.ShowProgress("Writing EEPROM...");

                bool programmed = await Task.Run(() =>
                    SetChannel(card.Channel) &&
                    _pga305.ProgramDevice(result.coefficients, config.SelectedRegisters));

                if (!programmed)
                {
                    card.ShowProgress("EEPROM write FAILED, nothing written to database");
                    Fail(card, "device programming failed, nothing written to database");
                    return;
                }

                card.ShowProgress("EEPROM updated");
                card.ShowProgress("Writing transducer to database...");

                bool transducer = await _api.CreateTransducer(
                    config.JobCode,
                    result.serial_number,
                    config.ElectricalOutput,
                    $"{config.pMin}-{config.pMax} {config.PressureUnit}",
                    config.SignalType);

                if (!transducer)
                {
                    card.ShowProgress("Transducer write FAILED");
                    Fail(card, "transducer write failed, skipping coefficients");
                    return;
                }

                card.ShowProgress("Transducer created");
                card.ShowProgress("Writing coefficients to database...");

                bool finalCoeff = await _api.CreateFinalCoefficients(
                    result.session_id, result.serial_number, config.JobCode,
                    result.coefficients, result.padc_gain, result.tadc_gain,
                    result.padc_offset, result.tadc_offset);

                if (!finalCoeff)
                {
                    card.ShowProgress("Coefficient write FAILED");
                    Fail(card, "final coefficients write failed");
                    return;
                }

                card.ShowProgress("Database updated");
                card.ShowProgress($"Configured {DateTime.Now:yyyy-MM-dd HH:mm}");

                card.DeviceConnected = false;
                card.BorderColor = Color.Green;
                ReleaseBus();
            }
            catch (Exception ex)
            {
                card.ShowProgress($"Error: {ex.Message}");
                Fail(card, ex.Message);
            }
        }

        private void Fail(ChannelCard card, string reason)
        {
            Debug.WriteLine($"Channel {card.Channel}: {reason}");
            card.BorderColor = Color.Red;
            card.SetInteractive(true);
        }

        private static bool PressureRangeIsLegal(PGAOutputConfig config)
        {
            int limit = config.PressureUnit == "bar" ? config.maxBar : config.maxPSI;

            return config.pMin >= 0
                && config.pMin < config.pMax
                && (limit == 0 || config.pMax <= limit);
        }
    }
}