using System.Text.Json.Serialization;

namespace PGA305OWICalibration.Models
{

    public class StockCode
    {
        public string stock_code { get; set; } = "";
        public string output_type { get; set; } = "";
        public double output_min { get; set; }
        public double output_max { get; set; }
        public string? pressure_reference { get; set; }
        public string pressure_units { get; set; } = "";
        public double pressure_min { get; set; }
        public double pressure_max { get; set; }
        public string? pressure_code { get; set; }
        public string? accuracy { get; set; }
    }
}