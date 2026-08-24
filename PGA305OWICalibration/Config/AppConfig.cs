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
        public static bool TestMode => bool.Parse(_config["TestMode"] ?? "false");

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
        //this will need to be moved to a JSON file or something else because there is about 10 fixed ones
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
    }
}