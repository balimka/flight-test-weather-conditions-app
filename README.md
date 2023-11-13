Weather conditions for
aircraft flight test

Assessment task
By Hitachi Solutions

You are preparing for a test flight. You are in the flight control centre. Your task is to calculate which is the
most appropriate day for the test flight based on the weather conditions. You have the weather forecast for
the first half of July and the weather criteria for a successful test.
Create the following C# (.NET Core) Console Application:
 The application should take 4 input parameters – File name (path to the file on the file system),
Sender email address, Password, Receiver email address.
 The type of the accepted input file for the weather forecast (filename parameter) is CSV and has the
following structure (this is sample data; you can create your own, e.g. for the whole month):

 The criteria for the weather conditions for a rocket launch is as follows:
• Temperature between 2 and 31 degrees Celsius;
• Wind speed no more than 10m/s (the lower the better);
• Humidity less than 60% (the lower the better);
• No precipitation;
• No lightings;
• No cumulus or nimbus clouds.

Day/Parameter 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15
Temperature (C) 28 28 29 30 31 32 31 30 28 28 27 29 31 32 32
Wind (m/s) 15 13 12 14 11 10 6 5 4 3 2 3 2 2 2
Humidity (%) 20 30 30 35 60 70 80 60 30 20 25 20 15 15 20
Precipitation (%) 0 0 0 0 20 40 30 20 0 0 0 5 5 0 0
Lightning No No No No No Yes Yes No No No No No No No No
Clouds Cumulus Cumulus Stratus Stratus Stratus Nimbus Nimbus Stratus Cumulus Cirrus Cumulus Stratus Cirrus Cirrus Cirrus

 The application should calculate the most appropriate date for the test flight based on the above
criteria and create new CSV file named “WeatherReport.csv” containing the same Parameters and
for every Parameter aggregate the data for the given period as such:
• Average value
• Median value
• Min value
• Max value
• Most appropriate day parameter value
 For the non-number parameters, leave the aggregates blank but populate the flight date values.
 The proposed most appropriate flight date and newly generated csv file should be sent to the email
(4th input parameter). This will happen by using the 2nd and 3rd input parameters (Sender mail and
Password) to establish connection using SMTP and send the file as attachment to the email. Hint:
using Gmail smtp could be difficult because they have additional security. Try other service like
Outlook.com, for example.
 The completed application source code should be sent as exported project or uploaded to accessible
version control platform (GitHub, for example) for review.

Bonus tasks:
 Make the application UI multilingual (English & German) with the ability to change the language.
 Allow weather criteria (part or all of it) to be entered as input to enable more flexibility.

Considerations:
 Application should provide user-friendly experience.
 Implement error handling (for example, a simple message “File is not found” instead of unhandled
error and printed call stack).
 Unit Tests are nice to have.
 Performance of the application will be considered.
 Time taken for completion will be considered.
