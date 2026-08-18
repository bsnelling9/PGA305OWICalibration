using PGA305OWICalibration.Config;
using PGA305OWICalibration.Instruments;

namespace PGA305OWICalibration.PGA305
{
    internal class PGA305_I2C
    {
        private readonly Stm32I2cController _stm32;

        public PGA305_I2C(Stm32I2cController stm32)
        {
            _stm32 = stm32;
        }

        public bool EnterCommandMode(int channel)
        {
            _stm32.SetChannel(1);
            return _stm32.EnterCommandMode(EEPROMRegister.I2C_RUNTIME_ADDR);
        }

        public string? ReadSerialNumber()
        {
            for (int attempt = 0; attempt < 3; attempt++)
            {
                int? b0 = _stm32.ReadRegister(EEPROMRegister.INTERNAL_SN_B0, EEPROMRegister.I2C_EEPROM_ADDR);
                int? b1 = _stm32.ReadRegister(EEPROMRegister.INTERNAL_SN_B1, EEPROMRegister.I2C_EEPROM_ADDR);
                int? b2 = _stm32.ReadRegister(EEPROMRegister.INTERNAL_SN_B2, EEPROMRegister.I2C_EEPROM_ADDR);
                int? b3 = _stm32.ReadRegister(EEPROMRegister.INTERNAL_SN_B3, EEPROMRegister.I2C_EEPROM_ADDR);

                if (b0 == null || b1 == null || b2 == null || b3 == null) return null;

                int serial = b0.Value + (b1.Value << 8) + (b2.Value << 16) + (b3.Value << 24);
                if (serial != 0)
                    return serial.ToString();
            }
            return null;
        }

        public string? ReadSensorSerialNumber()
        {
            for (int attempt = 0; attempt < 3; attempt++)
            {
                int? b0 = _stm32.ReadRegister(EEPROMRegister.SENSOR_SN_B0, EEPROMRegister.I2C_EEPROM_ADDR);
                int? b1 = _stm32.ReadRegister(EEPROMRegister.SENSOR_SN_B1, EEPROMRegister.I2C_EEPROM_ADDR);
                int? b2 = _stm32.ReadRegister(EEPROMRegister.SENSOR_SN_B2, EEPROMRegister.I2C_EEPROM_ADDR);

                if (b0 == null || b1 == null || b2 == null) return null;

                int serial = b0.Value + (b1.Value << 8) + (b2.Value << 16);
                if (serial != 0)
                    return serial.ToString();
            }

            return null;
        }

        public string? ReadPressureCode()
        {
            for (int attempt = 0; attempt < 3; attempt++)
            {
                int? lsb = _stm32.ReadRegister(EEPROMRegister.PRANGE_LSB, EEPROMRegister.I2C_EEPROM_ADDR);
                int? msb = _stm32.ReadRegister(EEPROMRegister.PRANGE_MSB, EEPROMRegister.I2C_EEPROM_ADDR);

                if (lsb == null || msb == null) continue;

                int prange = lsb.Value + (msb.Value << 8);

                if (prange != 0)
                    return prange.ToString();
            }
            return null;
        }

        public void Disconnect() => _stm32.DisconnectChannel();
    }
}