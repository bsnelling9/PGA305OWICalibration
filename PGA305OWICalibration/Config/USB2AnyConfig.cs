
namespace PGA305OWICalibration.Config
{
    internal class USB2AnyConfig
    {
        // OWI timing constants in microseconds — OWI-protocol specific, stay local
        public const ushort TIME_SETUP = 1000;
        public const ushort TIME_STORE = 1000;
        public const int FLAGS = 0;
        public const ushort OW_MODE = 5;
        public const ushort ACT_TIME_LOW = 1000;
        public const ushort ACT_TIME_HIGH = 1000;

        // GPIO pin function/state codes — protocol-level, stay local
        public const byte FN_OUTPUT = 1;
        public const byte FN_INPUT = 2;
        public const byte FN_INPUT_PULLUP = 3;
        public const byte STATE_HIGH = 2;
        public const byte STATE_LOW = 1;

        // PGA305 OWI command bytes — protocol-level, stay local
        public const byte SYNC_BYTE = 0x55;
        public const byte CMD_WRITE = 0x51;
        public const byte CMD_READ_INIT = 0x52;
        public const byte CMD_READ_RESPONSE = 0x73;
        public const byte CMD_WRITE_PAGE0 = 0x01;

        /*
       GPIO4 is used for OWI Tx
       GPIO5 is used for OWI Rx
       GPIO7 is used for OWI activation pulse (GPIO_OWI_ACT)
       GPIO10 is used for OWI VDD control (GPIO_OWI_VDD)
       GPIO11 is used for OWI TX control (GPIO_OWI_TX)
       */
        public const byte GPIO4 = 4;
        public const byte GPIO5 = 5;
        public const byte GPIO7 = 7;
        public const byte GPIO10 = 10;
        public const byte GPIO11 = 11;
    }
}
