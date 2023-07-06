using flight_test_weather_conditions_app.Models;
using flight_test_weather_conditions_app.Services.Interfaces;

namespace flight_test_weather_conditions_app.Services
{
    public class WeatherReportService : IWeatherReportService
    {

        public List<Day> ReadWeatherDataFromCSV(string fileName)
        {
            List<Day> weatherData = new();

            string[] lines = File.ReadAllLines(fileName);
            string[] headers = lines.First(x => x.Contains("Day/Parameter")).Split(',');

            string[] temperatures = lines.First(x => x.Contains("Temperature")).Split(',');
            string[] winds = lines.First(x => x.Contains("Wind")).Split(',');
            string[] humidity = lines.First(x => x.Contains("Humidity")).Split(',');
            string[] precipitation = lines.First(x => x.Contains("Precipitation")).Split(',');
            string[] lightning = lines.First(x => x.Contains("Lightning")).Split(',');
            string[] clouds = lines.First(x => x.Contains("Clouds")).Split(',');

            for (int i = 1; i < headers.Length; i++)
            {
                Day day = new()
                {
                    Id = int.Parse(headers[i]),
                    Temperature = double.Parse(temperatures[i]),
                    WindSpeed = double.Parse(winds[i]),
                    Humidity = double.Parse(humidity[i]),
                    Precipitation = double.Parse(precipitation[i]),
                    Lightning = lightning[i],
                    Clouds = clouds[i]
                };

                weatherData.Add(day);
            }

            return weatherData;
        }

        public void GenerateWeatherReport(List<Day> weatherData, string reportFileName, Day bestDay)
        {
            using StreamWriter writer = new(reportFileName);
            writer.WriteLine($"Parameter,Average,Median,Min,Max,Best Flight Date - {bestDay.Id}");

            string[] parameters = { "Temperature (C)", "Wind (m/s)", "Humidity (%)", "Precipitation (%)", "Lightning", "Clouds" };
            foreach (string parameter in parameters)
            {
                List<double> values = weatherData.Select(day =>
                {
                    return parameter switch
                    {
                        "Temperature (C)" => day.Temperature,
                        "Wind (m/s)" => day.WindSpeed,
                        "Humidity (%)" => day.Humidity,
                        "Precipitation (%)" => day.Precipitation,
                        _ => 0.0,
                    };
                }).ToList();

                double average = values.Average();
                double median = values.Count % 2 == 0 ? (values[values.Count / 2 - 1] + values[values.Count / 2]) / 2 : values[values.Count / 2];
                double min = values.Min();
                double max = values.Max();

                writer.WriteLine($"{parameter},{average:F2},{median:F2},{min},{max},{GetDayData(bestDay, parameter)}");
            }
        }

        private static string GetDayData(Day day, string parameter)
        {
            if (day != null)
            {
                switch (parameter)
                {
                    case "Temperature (C)":
                        return day.Temperature.ToString();
                    case "Wind (m/s)":
                        return day.WindSpeed.ToString();
                    case "Humidity (%)":
                        return day.Humidity.ToString();
                    case "Precipitation (%)":
                        return day.Precipitation.ToString();
                    case "Lightning":
                        return day.Lightning;
                    case "Clouds":
                        return day.Clouds;
                }
            }

            return "";
        }
    }
}
