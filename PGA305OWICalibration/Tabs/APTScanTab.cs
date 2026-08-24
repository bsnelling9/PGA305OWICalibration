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
        
        // Move these to a config file
        private const int ChannelCount = 8;
        private const int CompensationSettleMs = 10;
        private const int RelaySettleMs = 20;
        private const int ChannelSettleMs = 10;
        private const int DeviceSettleMs = 500;

        private readonly ApiClient _api = new ApiClient();
        private readonly StockCodes _stockCodes = new StockCodes();
        private readonly STM32Controller _stm32;
        private readonly PGA305Device _pga305;

        private readonly ChannelCard[] _cards = new ChannelCard[ChannelCount];

        private ChannelCard? _activeCard;
        private bool _cardBusy;
        private bool _batchRunning;

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

                card.ConnectRequested += async (s, e) => await ConnectChannel((ChannelCard)s!);
                card.ConfigureRequested += async (s, e) => await ConfigureChannel((ChannelCard)s!);

                _cards[i] = card;
                tlpCards.Controls.Add(card, i % 4, i / 4);
            }
        }

        private bool SetActiveChannelCard(ChannelCard card)
        {
            if (_activeCard != null && _activeCard != card)
                return false;

            _activeCard = card;
            UpdateCardsDisplay();
            
            return true;
        }

        private void ClearActiveCard()
        {
            _activeCard = null;
            UpdateCardsDisplay();
        }

        private void UpdateCardsDisplay()
        {
            foreach (var card in _cards)
                card.SetInteractive(
                    !_cardBusy && !_batchRunning
                    && (_activeCard == null || _activeCard == card));
        }

        private bool SelectMuxChannel(int channel)
        {
            if (!_stm32.SelectChannel(channel))
            {
                Debug.WriteLine($"Failed to select Channel {channel}");
                return false;
            }

            Debug.WriteLine($"Channel {channel} selected, mux register 0x{_stm32.CurrentConfig:X2}");
            return true;
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

            string newType = spec.Output.Type.ToLowerInvariant() switch
            {
                "ratiometric" => PGAOutputConfig.Ratiometric,
                "voltage" => PGAOutputConfig.Voltage,
                "current" => PGAOutputConfig.Current,
                _ => string.Empty
            };

            if (newType.Length == 0)
            {
                card.BorderColor = Color.Red;
                card.ShowMessage($"Stock code '{spec.StockCode}' has unknown output type '{spec.Output.Type}'.");
                return false;
            }

            if (card.DeviceConnected && newType != config.SignalType)
            {
                card.BorderColor = Color.Red;
                card.ShowMessage($"'{spec.StockCode}' is {newType}, device connected as {config.SignalType}.{Environment.NewLine}Disconnect to change output type.");
                return false;
            }

            switch (newType)
            {
                case PGAOutputConfig.Ratiometric:
                    config.SelectRatiometric();
                    break;

                case PGAOutputConfig.Current:
                    config.SelectCurrent();
                    break;

                case PGAOutputConfig.Voltage:
                    string voltageRange = $"{spec.Output.Min:0.##}-{spec.Output.Max:0.##}V";
                    try
                    {
                        config.SelectVoltage(voltageRange);
                    }
                    catch (ArgumentException)
                    {
                        card.BorderColor = Color.Red;
                        card.ShowMessage($"'{spec.StockCode}' asks for {voltageRange}, which isn't a configured voltage range.");
                        return false;
                    }
                    break;
            }

            config.StockCode = spec.StockCode;
            config.PressureUnit = spec.Pressure.Units;
            config.pMin = (int)spec.Pressure.Min;
            config.pMax = (int)spec.Pressure.Max;

            card.ShowMessage(string.Empty);
            return true;
        }

        private async Task<bool> ConnectChannel(ChannelCard card)
        {
            if (card is StockCodeCard stockCard)
            {
                if (!LoadStockCode(stockCard))
                    return false;

                if (card.DeviceConnected)
                {
                    card.UpdateDisplay();
                    return true;
                }
            }

            if (!_stm32.IsConnected)
            {
                card.BorderColor = Color.Goldenrod;
                Debug.WriteLine("STM32 not connected, connect hardware first");
                return false;
            }

            if (!SetActiveChannelCard(card))
                return false;

            card.BorderColor = Color.RoyalBlue;

            bool ok = await Task.Run(() => ConnectDevice(card.Channel, card.OutputConfig));

            if (!ok)
            {
                card.DeviceConnected = false;
                card.ShowProgress("Failed to activate");
                card.BorderColor = Color.Red;
                ClearActiveCard();
                return false;
            }

            card.DeviceConnected = true;
            return true;
        }

        private bool ConnectDevice(int channel, PGAOutputConfig config)
        {
            try
            {
                _pga305.ParkLines();
                Thread.Sleep(DeviceSettleMs);

                if (!_stm32.ConfigureMuxForOWI(config.SignalType))
                    return false;

                if (!SelectMuxChannel(channel))
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

        private async Task<bool> ConfigureChannel(ChannelCard card)
        {
            if (_activeCard != card)
                return false;

            var config = card.OutputConfig;

            if (!config.PressureRangeIsValid)
            {
                card.BorderColor = Color.Red;
                Debug.WriteLine($"Channel {card.Channel}: {config.pMax} {config.PressureUnit} exceeds code {config.PressureCode}");
                return false;
            }

            _cardBusy = true;
            UpdateCardsDisplay();
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
                    return Fail(card, "convert-output failed");
                }

                if (result.serial_number != config.SerialNumber)
                {
                    card.ShowProgress($"Serial mismatch: expected {config.SerialNumber}, got {result.serial_number}");
                    return Fail(card, "serial number mismatch");
                }

                card.ShowProgress("Coefficients received");
                card.ShowProgress("Writing EEPROM...");

                bool programmed = await Task.Run(() => _pga305.ProgramDevice(result.coefficients, config.SelectedRegisters));

                if (!programmed)
                {
                    card.ShowProgress("EEPROM write FAILED, nothing written to database");
                    return Fail(card, "device programming failed");
                }

                card.ShowProgress("EEPROM updated");
                card.ShowProgress("Writing transducer to database...");

                bool transducer = await _api.CreateTransducer(
                    config.StockCode,
                    result.serial_number,
                    config.ElectricalOutput,
                    $"{config.pMin}-{config.pMax} {config.PressureUnit}",
                    config.SignalType);

                if (!transducer)
                {
                    card.ShowProgress("Transducer write FAILED");
                    return Fail(card, "transducer write failed");
                }

                card.ShowProgress("Transducer created");
                card.ShowProgress("Writing coefficients to database...");

                bool finalCoeff = await _api.CreateFinalCoefficients(
                    result.session_id, result.serial_number, config.StockCode,
                    result.coefficients, result.padc_gain, result.tadc_gain,
                    result.padc_offset, result.tadc_offset);

                if (!finalCoeff)
                {
                    card.ShowProgress("Coefficient write FAILED");
                    return Fail(card, "final coefficients write failed");
                }

                card.ShowProgress("Database updated");
                card.ShowProgress($"Configured {DateTime.Now:yyyy-MM-dd HH:mm}");

                card.DeviceConnected = false;
                card.BorderColor = Color.Green;
                ClearActiveCard();
                return true;
            }
            catch (Exception ex)
            {
                card.ShowProgress($"Error: {ex.Message}");
                return Fail(card, ex.Message);
            }
            finally
            {
                _cardBusy = false;
                UpdateCardsDisplay();
            }
        }

        private async void btnConfigureAll_Click(object? sender, EventArgs e)
        {
            string code = txtBatchStockCode.Text.Trim();

            var targets = _cards
                .OfType<StockCodeCard>()
                .Where(c => c.Included)
                .ToList();

            if (code.Length == 0)
            {
                Debug.WriteLine("Batch aborted: no stock code entered");
                return;
            }

            if (targets.Count == 0)
            {
                Debug.WriteLine("Batch aborted: no channels selected");
                return;
            }

            if (targets.Any(c => c.DeviceConnected))
            {
                Debug.WriteLine("Batch aborted: disconnect all selected channels first");
                return;
            }

            _batchRunning = true;
            btnConfigureAll.Enabled = false;

            foreach (var c in _cards)
                c.SetInteractive(false);

            Debug.WriteLine($"Batch: {targets.Count} channels selected, code '{code}'");

            try
            {
                foreach (var card in targets)
                {
                    Debug.WriteLine($"Batch: starting channel {card.Channel}");

                    try
                    {
                        card.SetStockCode(code);

                        if (await ConnectChannel(card))
                            await ConfigureChannel(card);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Channel {card.Channel} batch error: {ex}");
                        card.BorderColor = Color.Red;
                        card.ShowProgress($"Error: {ex.Message}");
                    }
                    finally
                    {
                        _activeCard = null;
                    }

                    Debug.WriteLine($"Batch: finished channel {card.Channel}");
                }
            }
            finally
            {
                _batchRunning = false;
                btnConfigureAll.Enabled = true;
                ClearActiveCard();

                foreach (var card in targets)
                    card.ClearSelection();

                Debug.WriteLine("Batch: complete");
            }
        }

        private bool Fail(ChannelCard card, string reason)
        {
            Debug.WriteLine($"Channel {card.Channel}: {reason}");
            card.BorderColor = Color.Red;

            return false;
        }              

        private void BatchMode_CheckedChanged(object sender, EventArgs e)
        {
            bool batch = rbnBatchConfigure.Checked;

            foreach (var card in _cards.OfType<StockCodeCard>())
                card.SelectionMode = batch;

            txtBatchStockCode.Enabled = batch;
            btnConfigureAll.Enabled = batch;
        }

        private void rbnBatchConfigure_CheckedChanged(object sender, EventArgs e)
            => BatchMode_CheckedChanged(sender, e);

        private void rbnConfigureSingle_CheckedChanged(object sender, EventArgs e)
            => BatchMode_CheckedChanged(sender, e);
    }
}