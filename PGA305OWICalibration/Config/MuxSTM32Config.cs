
namespace PGA305OWICalibration.Config
{
    internal class MuxSTM32Config
    {
        public const byte OwiRelayBit = 0x40; // PB4  - OWI relay
        public const byte MaRelayBit = 0x20;  // PA11 - mA relay
        public const byte VoRelayBit = 0x10;  // PA8  - VO relay
        public const byte MEASRV_MASK = 0x0C;  // PB0  - measure reference voltage
        public const byte MeasureRvBit = 0x80; // MEASRV   PB0 measure reference voltage
        public const byte MeasureVoBit = 0x08;  // PA12 - measure voltage output
        public const byte MeasureMaBit = 0x04;  // PB7  - measure mA
        public const byte VCompA1Bit = 0x02;  // PA4  - voltage comparator 1
        public const byte VCompA0Bit = 0x01;  // PA5  - voltage comparator 0

        public const int ComparatorSettleMs = 10;
        public const int RelaySettleMs = 20;
        public const int ChannelSettleMs = 20;
        public const int CompensationSettleMs = 10;
        public const int DeviceSettleMs = 500;
        public const int ChannelCount = 8;

        public const byte RelayMask = OwiRelayBit | MaRelayBit | VoRelayBit;      
        public const byte ComparatorMask = VCompA0Bit | VCompA1Bit;

        //this is for testing but will need to make it simpler
        public static readonly Dictionary<string, (bool VCompA0High, bool VCompA1High)> Compensation = new()
        {
            ["Ratiometric"] = (true, true),
            ["Voltage"] = (true, false),
            ["Current"] = (false, true),
        };
    }
}
