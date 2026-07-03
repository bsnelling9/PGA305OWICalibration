using PGA305OWICalibration.Config;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace PGA305OWICalibration.API
{
    public class ConvertOutputResult
    {
        public int session_id { get; set; }
        public int serial_number { get; set; }
        public Dictionary<string, string> coefficients { get; set; } = new();
        public string padc_gain { get; set; } = "";
        public string tadc_gain { get; set; } = "";
        public string padc_offset { get; set; } = "";
        public string tadc_offset { get; set; } = "";
    }

    public class ApiClient
    {
        private readonly HttpClient _client = new();

        public async Task<ConvertOutputResult?> ConvertOutput(int serialNumber, double voltageMin, double voltageMax, double pressureMin, double pressureMax, string pressureUnit)
        {
            var payload = new
            {
                serial_number = serialNumber,
                v_min = voltageMin,
                v_max = voltageMax,
                p_min = pressureMin,
                p_max = pressureMax,
                pressure_unit = pressureUnit
            };
            string json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{AppConfig.API_URL}/convert-output", content);
            if (!response.IsSuccessStatusCode) return null;

            string resultJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ConvertOutputResult>(resultJson);
        }

        private static int HexToSignedInt24(string hex)
        {
            int value = Convert.ToInt32(hex, 16);
            if (value >= 0x800000)
                value -= 0x1000000;
            return value;
        }

        public async Task<bool> CreateFinalCoefficients(
            int sessionId, int serialNumber, string? stockCode,
            Dictionary<string, string> coefficients,
            string padcGain, string tadcGain, string padcOffset, string tadcOffset)
        {
            var payload = new Dictionary<string, object?>
            {
                ["session_id"] = sessionId,
                ["serial_number"] = serialNumber,
                ["stock_code"] = stockCode,
                ["padc_gain"] = HexToSignedInt24(padcGain),
                ["tadc_gain"] = HexToSignedInt24(tadcGain),
                ["padc_offset"] = HexToSignedInt24(padcOffset),
                ["tadc_offset"] = HexToSignedInt24(tadcOffset)
            };

            foreach (var kv in coefficients)
                payload[kv.Key] = HexToSignedInt24(kv.Value);

            string json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{AppConfig.API_URL}/final-coefficients", content);
            Debug.WriteLine(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateTransducer(
            string stockCode, int serialNumber, string electricalOutput,
            string pressureRange, string outputConfiguration)
        {
            var payload = new
            {
                stock_code = stockCode,
                serial_number = serialNumber,
                electrical_output = electricalOutput,
                pressure_range = pressureRange,
                output_configuration = outputConfiguration,
                final_cal_timestamp = DateTime.UtcNow,
                model_number = ""
            };

            string json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{AppConfig.API_URL}/transducer", content);
            Debug.WriteLine(response);
            return response.IsSuccessStatusCode;
        }
    }
}