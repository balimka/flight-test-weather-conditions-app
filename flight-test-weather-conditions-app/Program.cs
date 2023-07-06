using flight_test_weather_conditions_app;
using flight_test_weather_conditions_app.Services;
using flight_test_weather_conditions_app.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestFlightWeatherAssessment
{
    class Program
    {
        static void Main()
        {
            var builder = new ServiceCollection()
                .AddSingleton<Engine>()
                .AddSingleton<IEmailService, EmailService>()
                .AddSingleton<IWeatherReportService, WeatherReportService>()
                .AddSingleton<IWeatherUtilityService, WeatherUtilityService>()
                .AddSingleton<IConfiguration>(new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile("translation.json")
                    .Build())
                .BuildServiceProvider();

            Engine app = builder.GetRequiredService<Engine>();
            app.Run();
        }

    }
}
