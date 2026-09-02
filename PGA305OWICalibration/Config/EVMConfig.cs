
namespace PGA305OWICalibration.Config
{
    internal class EVMConfig
    {
        //Rloop
        public const int RLOOP_ADDR = 0x2D;
        public const byte RLOOP_REG = 0x00;
        public const byte RLOOP_10R = 0x19;
        public const byte RLOOP_22R = 0x21;
        public const int RLOOP_80R = 0x44;

        // Additional Voltage
        public const int TPL0102_ADDR = 0x57;
        public const byte ADDVOLT_REG_WA = 0x00;
        public const byte ADDVOLT_REG_WB = 0x01;

        public const byte ADDVOLT_0V0 = 0x00;
        public const byte ADDVOLT_0V5 = 0x09;
        public const byte ADDVOLT_0V6 = 0x0C;
        public const byte ADDVOLT_0V7 = 0x0E;
        public const byte ADDVOLT_1V0 = 0x17;
    }
}
