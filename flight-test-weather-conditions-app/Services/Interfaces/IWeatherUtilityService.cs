using flight_test_weather_conditions_app.Models;

namespace flight_test_weather_conditions_app.Services.Interfaces
{
    public interface IWeatherUtilityService
    {
        void InitializeTranslations(Dictionary<string, string> translations);

        void SetWeatherCriteriaByUser();

        Day CalculateBestFlightDate(List<Day> weatherData);
    }
}
