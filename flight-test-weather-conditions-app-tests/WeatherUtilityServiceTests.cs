using flight_test_weather_conditions_app.Models;
using flight_test_weather_conditions_app.Services;

namespace flight_test_weather_conditions_app.Tests.Services
{
    public class WeatherUtilityServiceTests
    {
        private readonly WeatherUtilityService _weatherService;

        public WeatherUtilityServiceTests()
        {
            _weatherService = new WeatherUtilityService();
        }

        [Fact]
        public void CalculateBestFlightDate_ShouldReturnBestFlightDate_DefaultCriteria()
        {
            // Arrange
            var weatherData = new List<Day>
            {
                new Day { Id = 1, Temperature = 25, WindSpeed = 5, Humidity = 50, Precipitation = 0, Lightning = "No", Clouds = "Cumulus" },
                new Day { Id = 2, Temperature = 20, WindSpeed = 5, Humidity = 45, Precipitation = 0, Lightning = "No", Clouds = "Stratus" },
                new Day { Id = 3, Temperature = 30, WindSpeed = 10, Humidity = 60, Precipitation = 0, Lightning = "No", Clouds = "Nimbus" }
            };

            // Act
            var bestFlightDate = _weatherService.CalculateBestFlightDate(weatherData);

            // Assert
            Assert.Equal(2, bestFlightDate.Id);
        }

        [Fact]
        public void CalculateBestFlightDate_ShouldReturnNull_WhenNoSuitableDaysFound_DefaultCriteria()
        {
            // Arrange
            var weatherData = new List<Day>
            {
                new Day { Id = 1, Temperature = 25, WindSpeed = 5, Humidity = 70, Precipitation = 0, Lightning = "No", Clouds = "Cumulus" },
                new Day { Id = 2, Temperature = 20, WindSpeed = 15, Humidity = 80, Precipitation = 0, Lightning = "No", Clouds = "Stratus" }
            };

            // Act
            var bestFlightDate = _weatherService.CalculateBestFlightDate(weatherData);

            // Assert
            Assert.Null(bestFlightDate);
        }

        [Fact]
        public void CalculateBestFlightDate_ShouldReturnFirstDay_WhenTwoDaysHasSameScore_DefaultCriteria()
        {
            // Arrange
            var weatherData = new List<Day>
            {
                new Day { Id = 1, Temperature = 25, WindSpeed = 5, Humidity = 50, Precipitation = 0, Lightning = "No", Clouds = "Stratus" },
                new Day { Id = 2, Temperature = 25, WindSpeed = 5, Humidity = 50, Precipitation = 0, Lightning = "No", Clouds = "Stratus" },
            };

            // Act
            var bestFlightDate = _weatherService.CalculateBestFlightDate(weatherData);

            // Assert
            Assert.Equal(1, bestFlightDate.Id);
        }

        [Fact]
        public void CalculateBestFlightDate_ShouldReturnLowestScoreDay_WhenMultipleSuitableDaysAvailable_DefaultCriteria()
        {
            // Arrange
            var weatherData = new List<Day>
            {
                new Day { Id = 1, Temperature = 25, WindSpeed = 10, Humidity = 60, Precipitation = 0, Lightning = "No", Clouds = "Stratus" },
                new Day { Id = 2, Temperature = 20, WindSpeed = 5, Humidity = 50, Precipitation = 0, Lightning = "No", Clouds = "Stratus" },
                new Day { Id = 3, Temperature = 25, WindSpeed = 7, Humidity = 55, Precipitation = 0, Lightning = "No", Clouds = "Stratus" }
            };

            // Act
            var bestFlightDate = _weatherService.CalculateBestFlightDate(weatherData);

            // Assert
            Assert.Equal(2, bestFlightDate.Id);
        }
    }
}