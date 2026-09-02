
namespace PGA305OWICalibration.Config
{
    internal class USB2AnyConfig
    {
        // UART Commands and Settings
        //SET MODE
        public const int UART_Normal = 0;
        public const int UART_ReceiverOFF = 1;
        public const int UART_RecvAfterXmit = 2;


        //OWI Settings
        //OWI SetOutput
        public const int SetOutput_State_Low = 0;
        
        // OWI timing constants in microseconds
        public const ushort TIME_SETUP = 1000;
        public const ushort TIME_STORE = 1000;
        public const int FLAGS = 0;
        public const ushort OW_MODE = 5;
        public const ushort ACT_TIME_LOW = 1000;
        public const ushort ACT_TIME_HIGH = 1000;

        //OWI Read and Write Commands
        public const byte SYNC_BYTE = 0x55;
        public const byte CMD_READ_RESPONSE = 0x73;
        public const byte CMD_WRITE_PAGE5 = 0x51;
        public const byte CMD_READ_INIT_PAGE5 = 0x52;
        public const byte CMD_WRITE_PAGE0 = 0x01;
        public const byte CMD_READ_PAGE0 = 0x02;
        public const byte CMD_BURST_WRITE_CACHE = 0xD0;
        public const byte CMD_BURST_READ_CACHE = 0xD3;

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

        // Pin Settings for GPIO Pins
        public const byte FN_OUTPUT = 1;
        public const byte FN_INPUT = 2;
        public const byte FN_INPUT_PULLUP = 3;
        public const byte STATE_HIGH = 2;
        public const byte STATE_LOW = 1;
    }
}
