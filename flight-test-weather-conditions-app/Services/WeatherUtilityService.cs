using flight_test_weather_conditions_app.Models;
using flight_test_weather_conditions_app.Services.Interfaces;

namespace flight_test_weather_conditions_app.Services
{
    public class WeatherUtilityService : IWeatherUtilityService
    {
        int temperatureMin = 2;
        int temperatureMax = 31;
        int windSpeed = 10;
        int humidityPercentage = 60;
        List<string> clouds = new() { "Cumulus", "Nimbus" };

        private Dictionary<string, string> _translations;

        public void InitializeTranslations(Dictionary<string, string> translations)
        {
            _translations = translations;
        }

        public void SetWeatherCriteriaByUser()
        {
            bool isValidInput = false;

            while (!isValidInput)
            {
                temperatureMin = GetValidIntegerInput(_translations["minTempMsg"]);
                temperatureMax = GetValidIntegerInput(_translations["maxTempMsg"]);
                windSpeed = GetValidIntegerInput(_translations["windSpeedMsg"]);
                humidityPercentage = GetValidIntegerInput(_translations["humidityMsg"]);

                Console.WriteLine(_translations["cloudsMsg"]);
                string cloudsInput = Console.ReadLine();

                if (!string.IsNullOrEmpty(cloudsInput) &&
                    (cloudsInput.Contains("Cumulus", StringComparison.OrdinalIgnoreCase)
                        || cloudsInput.Contains("Stratus", StringComparison.OrdinalIgnoreCase)
                        || cloudsInput.Contains("Nimbus", StringComparison.OrdinalIgnoreCase)
                        || cloudsInput.Contains("Cirrus", StringComparison.OrdinalIgnoreCase)
                        || cloudsInput.Contains("Anything", StringComparison.OrdinalIgnoreCase)))
                {
                    isValidInput = true;

                    clouds = cloudsInput
                        .Split(';', StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Trim())
                        .ToList();
                }
                else
                {
                    Console.WriteLine(_translations["invalidCloudMsg"]);
                }
            }
        }

        public Day CalculateBestFlightDate(List<Day> weatherData)
        {
            int bestFlightDate = 0;
            double bestScore = double.MaxValue;

            var suitableDays = weatherData
                .Where(day => day.Temperature >= temperatureMin && day.Temperature <= temperatureMax)
                .Where(day => day.WindSpeed <= windSpeed)
                .Where(day => day.Humidity < humidityPercentage)
                .Where(day => day.Precipitation == 0)
                .Where(day => day.Lightning == "No")
                .Where(day => clouds.Contains("Any") || !clouds.Contains(day.Clouds))
                .OrderBy(day => day.Id)
                .ToList();

            foreach (var day in suitableDays)
            {
                // score based on the given criteria (lower is better)
                double score = day.WindSpeed + day.Humidity / 10 + day.Precipitation * 10;

                if (score < bestScore)
                {
                    bestScore = score;
                    bestFlightDate = day.Id;
                }
            }

            return weatherData.FirstOrDefault(x => x.Id == bestFlightDate);
        }

        private int GetValidIntegerInput(string message)
        {
            int value = 0;
            bool isValidInput = false;

            while (!isValidInput)
            {
                Console.WriteLine(message);
                string input = Console.ReadLine();

                if (int.TryParse(input, out value))
                {
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine(_translations["invalidIntInput"]);
                }
            }

            return value;
        }
    }
}
