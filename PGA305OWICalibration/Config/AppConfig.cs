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
        public static bool TestMode => bool.Parse(_config["TestMode"] ?? "false");

        public static string STM32Port => _config["STM32COMPORT"] ?? "COM15";

        public static void SaveApiUrl(string newUrl)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            string json = File.ReadAllText(path);
            var node = System.Text.Json.Nodes.JsonNode.Parse(json)!;
            
            node["ApiUrl"] = newUrl;
            File.WriteAllText(path, node.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
        }

        public static void SaveMuxPort(string key, string value)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            string json = File.ReadAllText(path);
            var node = System.Text.Json.Nodes.JsonNode.Parse(json)!;

            node[key] = value;
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

        public static readonly Font ProgressFont = new Font("Segoe UI", 10F);
        public static readonly Font ResultFont = new Font("Segoe UI", 10F, FontStyle.Bold);

        public const int BAUD_RATE = 115200;
        public const string DEVICE_IDENTITY = "PGA305_MUX_OWI_01";             
    }
}