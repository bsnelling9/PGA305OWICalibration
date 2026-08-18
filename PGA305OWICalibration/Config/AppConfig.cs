using Microsoft.Extensions.Configuration;
using System.Text.Json;


// move the EEPROM addresses and values to another config specifically for the EEPROM
namespace PGA305OWICalibration.Config
{
    internal static class AppConfig
    {
        private static readonly IConfiguration _config;

        static AppConfig()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static string API_URL => _config["ApiUrl"] ?? "http://localhost:3000";

        public static void SaveApiUrl(string newUrl)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            string json = File.ReadAllText(path);
            var node = System.Text.Json.Nodes.JsonNode.Parse(json)!;
            node["ApiUrl"] = newUrl;
            File.WriteAllText(path, node.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
        }

        public class PressureRange
        {
            public int MaxPsi { get; init; }
            public int MaxBar { get; init; }
        }

        public static readonly Dictionary<string, PressureRange> PressureRanges = new()
        {
            ["16G"] = new PressureRange
            {
                MaxPsi = 232,
                MaxBar = 16
            },
            ["100G"] = new PressureRange
            {
                MaxPsi = 1500,
                MaxBar = 100
            }
        };


        public const int BAUD_RATE = 115200;
        public const string DEVICE_IDENTITY = "PGA305_Mux_01";

        // I2C devices on the EVM (DigiPot / TPL0102 signal conditioning)
        public const int DIGIPOT_ADDR = 0x2D;
        public const byte DIGIPOT_REG = 0x00;
        public const byte DIGIPOT_VALUE = 0x19;
        public const int TPL0102_ADDR = 0x57;

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
       GPIO5 is used for OWI Rx
       GPIO4 is used for OWI Tx
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