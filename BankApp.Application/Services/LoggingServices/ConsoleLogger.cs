namespace BankApp.Application.Services.LoggingServices
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message, LogLevel level)
        {
            Console.WriteLine($"{DateTime.Now} - {level}: {message}");
        }
    }
}

