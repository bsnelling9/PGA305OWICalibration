using PGA305OWICalibration.Config;

namespace PGA305OWICalibration.PGA305
{
    public class PGAOutputConfig
    {
        public const string Ratiometric = "Ratiometric";
        public const string Voltage = "Voltage";
        public const string Current = "Current";

        private static readonly Dictionary<byte, byte> RatiometricRegisters = new()
        {
            { EEPROMRegister.DAC_CONFIG.Address, 0x01 },
            { EEPROMRegister.OP_STAGE_CTRL.Address, 0x12 }
        };

        private static readonly Dictionary<byte, byte> CurrentRegisters = new()
        {
        };

        private static readonly Dictionary<string, (double Min, double Max)> VoltageRanges = new()
        {
            ["0-10V"] = (0.0, 10.0),
            ["0.5-4.5V"] = (0.5, 4.5),
            ["0-5V"] = (0.0, 5.0),
            ["1-5V"] = (1.0, 5.0),
            ["1-6V"] = (1.0, 6.0),
        };

        private static readonly Dictionary<string, Dictionary<byte, byte>> VoltageRegisters = new()
        {
            ["0-5V"] = new() { { EEPROMRegister.OP_STAGE_CTRL.Address, EEPROMRegister.DAC_GAIN_667V } },
            ["1-5V"] = new() { { EEPROMRegister.OP_STAGE_CTRL.Address, EEPROMRegister.DAC_GAIN_667V } },
            ["1-6V"] = new() { { EEPROMRegister.OP_STAGE_CTRL.Address, EEPROMRegister.DAC_GAIN_667V } },
        };

        public static IEnumerable<string> AvailableVoltageRanges => VoltageRanges.Keys;

        public int SerialNumber { get; set; }
        public string SensorSerialNumber { get; set; } = string.Empty;
        public string PressureCode { get; set; } = string.Empty;
        public string StockCode { get; set; } = string.Empty;
        public string JobCode { get; set; } = string.Empty;

        public int maxPSI { get; private set; }
        public int maxBar { get; private set; }

        public double vMin { get; private set; }
        public double vMax { get; private set; } = 10;

        public int pMin { get; set; }
        public int pMax { get; set; }

        public string SignalType { get; private set; } = string.Empty;
        public string ElectricalOutput { get; private set; } = string.Empty;

        public string PressureUnit { get; set; } = "psi";

        public Dictionary<byte, byte> SelectedRegisters { get; private set; } = new();

        public int MaxPressure => PressureUnit == "bar" ? maxBar : maxPSI;

        public bool PressureRangeIsValid =>
            pMin >= 0 && pMin < pMax && (MaxPressure == 0 || pMax <= MaxPressure);

        public void SelectRatiometric()
        {
            SignalType = Ratiometric;
            ElectricalOutput = "0.5-4.5V";
            vMin = 0.5;
            vMax = 4.5;
            SelectedRegisters = new Dictionary<byte, byte>(RatiometricRegisters);
        }

        public void SelectCurrent()
        {
            SignalType = Current;
            ElectricalOutput = "4-20mA";
            vMin = 0;
            vMax = 0;
            SelectedRegisters = new Dictionary<byte, byte>(CurrentRegisters);
        }

        public void SelectVoltage(string range)
        {
            if (!VoltageRanges.TryGetValue(range, out var limits))
                throw new ArgumentException($"Unknown voltage range '{range}'");

            SignalType = Voltage;
            ElectricalOutput = range;
            vMin = limits.Min;
            vMax = limits.Max;

            SelectedRegisters = VoltageRegisters.TryGetValue(range, out var registers)
                ? new Dictionary<byte, byte>(registers)
                : new Dictionary<byte, byte>();
        }

        public void SetPressureUnit(string unit)
        {
            PressureUnit = unit;
            pMin = 0;
            pMax = MaxPressure;
        }

        public void SetPressureRangeFromCode()
        {
            if (!AppConfig.PressureRanges.TryGetValue(PressureCode, out var range))
                throw new ArgumentException(
                    $"No pressure range configured for pressure code '{PressureCode}'");

            maxPSI = range.MaxPsi;
            maxBar = range.MaxBar;

            if (StockCode.Length == 0)
            {
                pMin = 0;
                pMax = MaxPressure;
            }
        }
    }
}