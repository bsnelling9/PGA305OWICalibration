using System.Text.Json;
using System.Text.Json.Serialization;

namespace PGA305OWICalibration.Config
{
    public class StockCodeSpec
    {
        [JsonPropertyName("stock_code")]
        public string StockCode { get; set; } = string.Empty;
        public OutputSpec Output { get; set; } = new();
        public PressureSpec Pressure { get; set; } = new();
    }

    public class OutputSpec
    {
        public double Min { get; set; }
        public double Max { get; set; }
        public string Units { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }

    public class PressureSpec
    {
        public double Min { get; set; }
        public double Max { get; set; }
        public string Units { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }

    public class StockCodeFile
    {
        [JsonPropertyName("stock_codes")]
        public Dictionary<string, StockCodeSpec> StockCodes { get; set; } = new();
    }

    public class StockCodes
    {
        private readonly Dictionary<string, StockCodeSpec> _codes;

        public StockCodes(string path = "stock-codes.json")
        {
            var full = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };

            var file = JsonSerializer.Deserialize<StockCodeFile>(File.ReadAllText(full), options);

            _codes = file != null
                ? new Dictionary<string, StockCodeSpec>(file.StockCodes, StringComparer.OrdinalIgnoreCase)
                : new Dictionary<string, StockCodeSpec>(StringComparer.OrdinalIgnoreCase);
        }

        public StockCodeSpec Lookup(string stockCode)
            => _codes.TryGetValue((stockCode ?? "").Trim(), out var spec) ? spec : null;
    }
}