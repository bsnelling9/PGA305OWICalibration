using PGA305OWICalibration.Config;

namespace PGA305OWICalibration.PGA305
{
    public class PGAOutputConfig
    {
        public const string Ratiometric = "Ratiometric";
        public const string Voltage = "Voltage";
        public const string Current = "Current";

        private const string Psi = "psi";
        private const string Bar = "bar";

        private const string RatiometricOutput = "0.5-4.5V";
        private const string CurrentOutput = "4-20mA";

        private sealed record OutputConfiguration(double Min, double Max, byte DacConfig, byte OpStageCtrl);

        private static readonly OutputConfiguration RatiometricSpec = new (0.5, 4.5, EEPROMRegister.DAC_MODE_RATIOMETRIC, EEPROMRegister.DAC_GAIN_4V);

        private static readonly OutputConfiguration CurrentSpec = new (4, 20, EEPROMRegister.DAC_MODE_ABSOLUTE, EEPROMRegister.CURRENT_MODE);

        private static readonly Dictionary<string, OutputConfiguration> VoltageSpecs = new()
        {
            ["0-10V"] = new(0.0, 10.0, EEPROMRegister.DAC_MODE_ABSOLUTE, EEPROMRegister.DAC_GAIN_10V),
            ["0.5-4.5V"] = new(0.5, 4.5, EEPROMRegister.DAC_MODE_ABSOLUTE, EEPROMRegister.DAC_GAIN_4V),
            ["0-5V"] = new(0.0, 5.0, EEPROMRegister.DAC_MODE_ABSOLUTE, EEPROMRegister.DAC_GAIN_667V),
            ["1-5V"] = new(1.0, 5.0, EEPROMRegister.DAC_MODE_ABSOLUTE, EEPROMRegister.DAC_GAIN_667V),
            ["1-6V"] = new(1.0, 6.0, EEPROMRegister.DAC_MODE_ABSOLUTE, EEPROMRegister.DAC_GAIN_667V)
        };

        public int SerialNumber { get; set; }
        public string SensorSerialNumber { get; set; } = string.Empty;
        public string PressureCode { get; set; } = string.Empty;
        public string StockCode { get; set; } = string.Empty;
        public string JobCode { get; set; } = string.Empty;

        public int MaxPsi { get; private set; }
        public int MaxBar { get; private set; }

        public double OutputMin { get; private set; }
        public double OutputMax { get; private set; }

        public int PressureMin { get; set; }
        public int PressureMax { get; set; }

        public string SignalType { get; private set; } = string.Empty;
        public string ElectricalOutput { get; private set; } = string.Empty;

        public string PressureUnit { get; set; } = Psi;

        public Dictionary<byte, byte> SelectedRegisters { get; private set; } = new();

        public int MaxPressure =>
            string.Equals(PressureUnit, Bar, StringComparison.OrdinalIgnoreCase) ? MaxBar : MaxPsi;

        public static IEnumerable<string> AvailableVoltageRanges => VoltageSpecs.Keys;

        public bool PressureRangeIsValid =>
            PressureMin >= 0
            && PressureMin < PressureMax
            && (MaxPressure == 0 || PressureMax <= MaxPressure);

        public void SelectRatiometric()
        {
            SetOutputConfiguration(Ratiometric, RatiometricOutput, RatiometricSpec);

            SelectedRegisters[EEPROMRegister.NORMAL_LOW_LSB_ADD] = OutputConfig.RATIO_NORMAL_LOW_LSB;
            SelectedRegisters[EEPROMRegister.NORMAL_LOW_MSB_ADD] = OutputConfig.RATIO_NORMAL_LOW_MSB;
            SelectedRegisters[EEPROMRegister.NORMAL_HIGH_LSB_ADD] = OutputConfig.RATIO_NORMAL_HIGH_LSB;
            SelectedRegisters[EEPROMRegister.NORMAL_HIGH_MSB_ADD] = OutputConfig.RATIO_NORMAL_HIGH_MSB;
            SelectedRegisters[EEPROMRegister.LOW_CLAMP_LSB_ADD] = OutputConfig.RATIO_LOW_CLAMP_LSB;
            SelectedRegisters[EEPROMRegister.LOW_CLAMP_MSB_ADD] = OutputConfig.RATIO_LOW_CLAMP_MSB;
            SelectedRegisters[EEPROMRegister.HIGH_CLAMP_LSB_ADD] = OutputConfig.RATIO_HIGH_CLAMP_LSB;
            SelectedRegisters[EEPROMRegister.HIGH_CLAMP_MSB_ADD] = OutputConfig.RATIO_HIGH_CLAMP_MSB;
        }        

        public void SelectCurrent() => SetOutputConfiguration(Current, CurrentOutput, CurrentSpec);

        public void SelectVoltage(string range)
        {
            if (!VoltageSpecs.TryGetValue(range, out var spec))
                throw new ArgumentException($"Unknown voltage range '{range}'");

            SetOutputConfiguration(Voltage, range, spec);
        }

        public void SetPressureUnit(string unit)
        {
            PressureUnit = unit;
            PressureMin = 0;
            PressureMax = MaxPressure;
        }

        public void SetPressureRangeFromCode()
        {
            if (!AppConfig.PressureRanges.TryGetValue(PressureCode, out var range))
                throw new ArgumentException(
                    $"No pressure range configured for pressure code '{PressureCode}'");

            MaxPsi = range.MaxPsi;
            MaxBar = range.MaxBar;

            if (StockCode.Length == 0)
            {
                PressureMin = 0;
                PressureMax = MaxPressure;
            }
        }

        private void SetOutputConfiguration(string signalType, string electricalOutput, OutputConfiguration spec)
        {
            SignalType = signalType;
            ElectricalOutput = electricalOutput;
            OutputMin = spec.Min;
            OutputMax = spec.Max;

            SelectedRegisters = new Dictionary<byte, byte>
            {
                { EEPROMRegister.DAC_CONFIG.Address, spec.DacConfig },
                { EEPROMRegister.OP_STAGE_CTRL.Address, spec.OpStageCtrl }
            };
        }
    }
}