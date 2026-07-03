using Microsoft.Extensions.Configuration;
using System.Text.Json;

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

        public const int BAUD_RATE = 115200;
        public const string DEVICE_IDENTITY = "PGA305_Mux_01";

        // ==========================================
        // I2C Addresses for the PGA305
        // ==========================================
        public const int I2C_RUNTIME_ADDR = 0x20;
        public const int I2C_CONTROL_ADDR = 0x22;
        public const int I2C_EEPROM_ADDR = 0x25;



        // ==========================================
        // EEPROM REGISTER ADDRESSES
        // ==========================================

        public const int TADC_GAIN_MSB = 0x60;
        public const int TADC_OFFSET_LSB = 0x61;
        public const int TADC_OFFSET_MID = 0x62;
        public const int TADC_OFFSET_MSB = 0x63;
        
        public const int INTERNAL_SN_B0 = 0x64;
        public const int INTERNAL_SN_B1 = 0x65;
        public const int INTERNAL_SN_B2 = 0x66;
        public const int INTERNAL_SN_B3 = 0x67;

        // Pressure Range (0x70-0x71)
        public const int PRANGE_LSB = 0x70;
        public const int PRANGE_MSB = 0x71;

        // Accuracy byte (0x72) ie G or +/- 0.2%
        public const int ACCURACY = 0x72;

        // Sensor Serial Number (0x73-0x75)
        public const int SENSOR_SN_B0 = 0x73;
        public const int SENSOR_SN_B1 = 0x74;
        public const int SENSOR_SN_B2 = 0x75;

        // EEPROM Cache 
        public const int CACHE_B0 = 0x80;
        public const int CACHE_B1 = 0x81;
        public const int CACHE_B2 = 0x82;
        public const int CACHE_B3 = 0x83;
        public const int CACHE_B4 = 0x84;
        public const int CACHE_B5 = 0x85;
        public const int CACHE_B6 = 0x86;
        public const int CACHE_B7 = 0x87;

        // EEPROM Control Registers
        public const int EEPROM_PAGE_ADDR = 0x88;
        public const int EEPROM_CTRL = 0x89;
        public const int EEPROM_CRC_TRIG = 0x8A;
        public const int EEPROM_STATUS = 0x8B;
        public const int EEPROM_CRC_STAT = 0x8C;
        public const int EEPROM_CRC_VAL = 0x8D;


        // I2C devices on the EVM (DigiPot / TPL0102 signal conditioning)
        public const int DIGIPOT_ADDR = 0x2D;
        public const byte DIGIPOT_REG = 0x00;
        public const byte DIGIPOT_VALUE = 0x19;
        public const int TPL0102_ADDR = 0x57;

        public const byte CMD_BURST_WRITE_CACHE = 0xD0;
        public const byte CMD_BURST_READ_CACHE = 0xD3;

        // EEPROM Control values
        public const int EEPROM_CTRL_ERASE_AND_PROGRAM = 0x04;

        // EEPROM Pages
        public const int PAGE_C = 0x0C;
        public const int PAGE_E = 0x0E;
        public const int PAGE_F = 0x0F;

        // Page F CRC byte address
        public const int PAGE_F_START = 0x78;
        public const int PAGE_F_CRC = 0x7F;

        public static readonly Dictionary<string, byte[]> COEFFICIENT_ADDRESSES = new()
        {
            ["h0"] = new byte[] { 0x00, 0x01, 0x02 },
            ["h1"] = new byte[] { 0x03, 0x04, 0x05 },
            ["h2"] = new byte[] { 0x06, 0x07, 0x08 },
            ["h3"] = new byte[] { 0x09, 0x0A, 0x0B },
            ["g0"] = new byte[] { 0x0C, 0x0D, 0x0E },
            ["g1"] = new byte[] { 0x0F, 0x10, 0x11 },
            ["g2"] = new byte[] { 0x12, 0x13, 0x14 },
            ["g3"] = new byte[] { 0x15, 0x16, 0x17 },
            ["n0"] = new byte[] { 0x18, 0x19, 0x1A },
            ["n1"] = new byte[] { 0x1B, 0x1C, 0x1D },
            ["n2"] = new byte[] { 0x1E, 0x1F, 0x20 },
            ["n3"] = new byte[] { 0x21, 0x22, 0x23 },
            ["m0"] = new byte[] { 0x24, 0x25, 0x26 },
            ["m1"] = new byte[] { 0x27, 0x28, 0x29 },
            ["m2"] = new byte[] { 0x2A, 0x2B, 0x2C },
            ["m3"] = new byte[] { 0x2D, 0x2E, 0x2F },
        };

        public static readonly byte[] PADC_GAIN_ADDR = { 0x44, 0x45, 0x46 };
        public static readonly byte[] PADC_OFFSET_ADDR = { 0x47, 0x48, 0x49 };
        public static readonly byte[] TADC_GAIN_ADDR = { 0x5E, 0x5F, 0x60 };
        public static readonly byte[] TADC_OFFSET_ADDR = { 0x61, 0x62, 0x63 };
    }
}