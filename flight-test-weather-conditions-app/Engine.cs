using flight_test_weather_conditions_app.Models;
using flight_test_weather_conditions_app.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace flight_test_weather_conditions_app
{
    public class Engine
    {
        private readonly IEmailService _emailService;
        private readonly IWeatherReportService _weatherReportService;
        private readonly IWeatherUtilityService _weatherUtility;
        private readonly IConfiguration _configuration;
        private Dictionary<string, string> _translations;

        public Engine(IEmailService emailService,
            IWeatherReportService weatherReportService,
            IConfiguration configuration,
            IWeatherUtilityService weatherUtility)
        {
            this._emailService = emailService;
            this._weatherReportService = weatherReportService;
            this._configuration = configuration;
            this._weatherUtility = weatherUtility;
        }

        public void Run()
        {
            GetUserLanguage();

            while (true)
            {
                string[] userArgs = GetUserDataInput();

                while (true)
                {
                    string filePath = userArgs[0];

                    bool doesFileExist = File.Exists(filePath);
                    bool hasCorrectExtension = Path.GetExtension(filePath) is ".csv";

                    if (!doesFileExist)
                    {
                        Console.WriteLine(_translations["fileDoesntExists"]);
                    }
                    else if (!hasCorrectExtension)
                    {
                        Console.WriteLine(_translations["fileWrongFormat"]);
                    }
                    else
                    {
                        break;
                    }

                    userArgs = GetUserDataInput();
                }

                Console.WriteLine(_translations["weatherQuestion"]);
                Console.WriteLine(_translations["optionsYesNo"]);
                string userInput = Console.ReadLine();

                if (string.Equals(userInput, _translations["yes"], StringComparison.OrdinalIgnoreCase) || userInput is "1")
                {
                    this._weatherUtility.SetWeatherCriteriaByUser();
                }

                try
                {
                    string fileName = userArgs[0];
                    string senderEmail = userArgs[1];
                    string password = userArgs[2];
                    string receiverEmail = userArgs[3];

                    List<Day> weatherData = this._weatherReportService.ReadWeatherDataFromCSV(fileName);

                    Day bestFlightDay = this._weatherUtility.CalculateBestFlightDate(weatherData);

                    if (bestFlightDay is null)
                    {
                        Console.WriteLine(_translations["notSuitableDayFound"]);
                    }
                    else
                    {
                        string reportFileName = "WeatherReport.csv";
                        this._weatherReportService.GenerateWeatherReport(weatherData, reportFileName, bestFlightDay);

                        string subject = _translations["mailSubject"];
                        string body = _translations["mailBody"];

                        this._emailService.SendEmailWithAttachment(senderEmail, password, receiverEmail, subject, body, reportFileName);

                        Console.WriteLine(_translations["mailSuccess"]);
                        Console.WriteLine(_translations["taskCompleted"]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(_translations["errorOccurred"] + ex.Message);
                }

                Console.WriteLine(_translations["endMessage"]);

                string input = Console.ReadLine();

                if (string.Equals(input, _translations["exit"], StringComparison.OrdinalIgnoreCase) || input is "1")
                {
                    break;
                }

                if (string.Equals(input, _translations["changeLang"], StringComparison.OrdinalIgnoreCase) || input is "3")
                {
                    GetUserLanguage();
                }
            }
        }

        private string[] GetUserDataInput()
        {
            string[] userArgs = null;
            Console.WriteLine(_translations["enterData"]);
            while (userArgs is null)
            {
                var input = Console.ReadLine();
                userArgs = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (userArgs.Length < 4)
                {
                    Console.WriteLine(_translations["insufficientArguments"]);
                    userArgs = null;
                }
            }

            return userArgs;
        }

        private void GetUserLanguage()
        {
            while (true)
            {
                Console.WriteLine("Please enter your language - options [1] EN / [2] DE");
                string lang = Console.ReadLine();

                if (string.Equals(lang, "EN", StringComparison.OrdinalIgnoreCase) || lang is "1")
                {
                    InitializeTranslations("EN");
                }
                else if (string.Equals(lang, "DE", StringComparison.OrdinalIgnoreCase) || lang is "2")
                {
                    InitializeTranslations("DE");
                }
                else
                {
                    Console.WriteLine("Invalid language option selected.");
                    continue;
                }
                break;
            }
        }

        private void InitializeTranslations(string language)
        {
            IConfigurationSection languageSection = this._configuration.GetSection(language);
            _translations = languageSection.Get<Dictionary<string, string>>();
            this._weatherUtility.InitializeTranslations(_translations);
        }

    }
}
