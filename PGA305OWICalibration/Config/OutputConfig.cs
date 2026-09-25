
namespace PGA305OWICalibration.Config
{
    internal class OutputConfig
    {
        // Ratiometric 0.5-4.5 V at 4 V/V gain.
        // Clamps sit 0.25 V outside the normal range.
        public const byte RATIO_NORMAL_LOW_LSB = 0x67;   // 0x0667 = 0.50 V
        public const byte RATIO_NORMAL_LOW_MSB = 0x06;
        public const byte RATIO_NORMAL_HIGH_LSB = 0x9A;   // 0x399A = 4.50 V
        public const byte RATIO_NORMAL_HIGH_MSB = 0x39;
        public const byte RATIO_LOW_CLAMP_LSB = 0x34;   // 0x0334 = 0.25 V
        public const byte RATIO_LOW_CLAMP_MSB = 0x03;
        public const byte RATIO_HIGH_CLAMP_LSB = 0xCF;   // 0x3CCF = 4.75 V
        public const byte RATIO_HIGH_CLAMP_MSB = 0x3C;
    }
}
