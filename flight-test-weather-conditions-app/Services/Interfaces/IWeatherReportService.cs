using flight_test_weather_conditions_app.Models;

namespace flight_test_weather_conditions_app.Services.Interfaces
{
    public interface IWeatherReportService
    {
        List<Day> ReadWeatherDataFromCSV(string fileName);

        void GenerateWeatherReport(List<Day> weatherData, string reportFileName, Day bestDay);
    }
}
