using PGA305OWICalibration.Config;

namespace PGA305OWICalibration.PGA305
{
    internal class PGAOutputConfig
    {
        public int SerialNumber { get; set; }
        public string PressureCode { get; set; } = string.Empty;

        public string JobCode { get; set; } = string.Empty;

        public int maxPSI { get; private set; }
        public int maxBar { get; private set; }

        public double vMin { get; private set; } = 0;
        public double vMax { get; private set; } = 10;

        public int pMin { get; set; }
        public int pMax { get; set; }

        public string SignalType { get; private set; } = string.Empty;
        public string ElectricalOutput { get; private set; } = string.Empty;

        //Has no effect on the convert output
        public string PressureUnit { get; set; } = "psiG";       

        public Dictionary<byte, byte> SelectedRegisters { get; private set; } = new();

        public Dictionary<byte, byte> Ratiometric { get; } = new()
        {
            { EEPROMRegister.DAC_CONFIG.Address, 0x01 },
            { EEPROMRegister.OP_STAGE_CTRL.Address, 0x12 }
        };
             
        public Dictionary<string, Dictionary<byte, byte>> Voltage { get; } = new()
        {
            ["1-6V"] = new()
            {    
                    { EEPROMRegister.OP_STAGE_CTRL.Address, EEPROMRegister.DAC_GAIN_667V},   
            },
            ["0-5V"] = new()
            {
                    { EEPROMRegister.OP_STAGE_CTRL.Address, EEPROMRegister.DAC_GAIN_667V},
            },
            ["1-5V"] = new()
            {
                    { EEPROMRegister.OP_STAGE_CTRL.Address, EEPROMRegister.DAC_GAIN_667V},
            },
        };

        public Dictionary<byte, byte> Current { get; } = new()
        {
            // Current register settings
        };

        public void SelectRatiometric()
        {
            SignalType = "Ratiometric";
            ElectricalOutput = "0.5-4.5V";

            vMin = 0.5;
            vMax = 4.5;

            SelectedRegisters = Ratiometric;
        }

        public void SelectCurrent()
        {
            SignalType = "Current";
            ElectricalOutput = "4-20mA";

            SelectedRegisters = Current;
        }

        public void ResetPressureRange()
        {
            pMin = 0;

            if (PressureUnit == "psiG")
                pMax = maxPSI;
            else if (PressureUnit == "bar")
                pMax = maxBar;
        }

        public void SelectVoltage(string range)
        {
            SignalType = "Voltage";
            ElectricalOutput = range;

            switch (range)
            {
                case "0-10V":
                    vMin = 0;
                    vMax = 10;
                    SelectedRegisters.Clear();
                    break;

                case "0-5V":
                    vMin = 0;
                    vMax = 5;
                    SelectedRegisters = Voltage[range];
                    break;

                case "1-6V":
                    vMin = 1;
                    vMax = 6;
                    SelectedRegisters = Voltage[range];
                    break;

                case "1-5V":
                    vMin = 1;
                    vMax = 5;
                    SelectedRegisters = Voltage[range];
                    break;

                case "0.5-4.5V":
                    vMin = 0.5;
                    vMax = 4.5;
                    SelectedRegisters.Clear();
                    break;

                default:
                    throw new ArgumentException(
                        $"Unknown voltage range '{range}'");
            }
        }

        public void SetPressureUnit(string unit)
        {
            PressureUnit = unit;

            if (unit == "psi")
            {
                pMax = maxPSI;
            }
            else if (unit == "bar")
            {
                pMax = maxBar;
            }
        }

        public void SetPressureRangeFromCode()
        {
            if (!AppConfig.PressureRanges.TryGetValue(PressureCode, out var range))
                throw new ArgumentException(
                    $"No pressure range configured for pressure code '{PressureCode}'");

            pMin = 0;
            pMax = range.MaxPsi;
            maxPSI = range.MaxPsi;
            maxBar = range.MaxBar;
        }
    }
}
