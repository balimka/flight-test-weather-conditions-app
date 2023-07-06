namespace flight_test_weather_conditions_app.Models
{
    public class Day
    {
        public int Id { get; set; }
        public double Temperature { get; set; }
        public double WindSpeed { get; set; }
        public double Humidity { get; set; }
        public double Precipitation { get; set; }
        public string Lightning { get; set; }
        public string Clouds { get; set; }
    }
}
