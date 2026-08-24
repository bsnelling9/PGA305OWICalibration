
namespace PGA305OWICalibration.Config
{
    internal class EVMConfig
    {
        public const int DIGIPOT_ADDR = 0x2D;
        public const byte DIGIPOT_REG = 0x00;
        public const byte DIGIPOT_VALUE = 0x19;

        //Rloop
        public const int RLOOP_ADDR = 0x2D;
        public const byte RLOOP_REG = 0x00;
        public const byte RLOOP_10R = 0x19;
        public const byte RLOOP_22R = 0x21;

        // Additional Voltage
        public const int TPL0102_ADDR = 0x57;
        public const byte ADDVOLT_REG_WA = 0x00;
        public const byte ADDVOLT_REG_WB = 0x01;

        public const byte ADDVOLT_0V0 = 0x00;
        public const byte ADDVOLT_0V5 = 0x09;
        public const byte ADDVOLT_0V6 = 0x0C;
        public const byte ADDVOLT_0V7 = 0x0E;
        public const byte ADDVOLT_1V0 = 0x17;

        // Ratiometric board — no series drop to compensate. Identical to legacy HandlePOT().
        public const byte RATIO_RLOOP = RLOOP_10R;
        public const byte RATIO_ADDV = ADDVOLT_0V0;

        // Voltage board — compensates R2 (22 Ω) + D2 (BAS16J) in the supply line.
        public const byte VOLT_RLOOP = RLOOP_22R;
        public const byte VOLT_ADDV = ADDVOLT_0V5;
    }
}
