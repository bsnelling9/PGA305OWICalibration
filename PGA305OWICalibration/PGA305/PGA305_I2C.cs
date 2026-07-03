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
            return _stm32.EnterCommandMode(AppConfig.I2C_RUNTIME_ADDR);
        }

        public string? ReadSerialNumber()
        {
            for (int attempt = 0; attempt < 3; attempt++)
            {
                int? b0 = _stm32.ReadRegister(AppConfig.INTERNAL_SN_B0, AppConfig.I2C_EEPROM_ADDR);
                int? b1 = _stm32.ReadRegister(AppConfig.INTERNAL_SN_B1, AppConfig.I2C_EEPROM_ADDR);
                int? b2 = _stm32.ReadRegister(AppConfig.INTERNAL_SN_B2, AppConfig.I2C_EEPROM_ADDR);
                int? b3 = _stm32.ReadRegister(AppConfig.INTERNAL_SN_B3, AppConfig.I2C_EEPROM_ADDR);

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
                int? b0 = _stm32.ReadRegister(AppConfig.SENSOR_SN_B0, AppConfig.I2C_EEPROM_ADDR);
                int? b1 = _stm32.ReadRegister(AppConfig.SENSOR_SN_B1, AppConfig.I2C_EEPROM_ADDR);
                int? b2 = _stm32.ReadRegister(AppConfig.SENSOR_SN_B2, AppConfig.I2C_EEPROM_ADDR);

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
                int? lsb = _stm32.ReadRegister(AppConfig.PRANGE_LSB, AppConfig.I2C_EEPROM_ADDR);
                int? msb = _stm32.ReadRegister(AppConfig.PRANGE_MSB, AppConfig.I2C_EEPROM_ADDR);

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