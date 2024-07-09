namespace BankApp.Application.Services.LoggingServices
{
	public class FileLogger : ILogger
	{
        private readonly string _filePath;

        public FileLogger(string filePath)
        {
            _filePath = filePath;
        }

        public void Log(string message, LogLevel level)
        {
            File.AppendAllText(_filePath, $"{DateTime.Now} - {level}: {message}{Environment.NewLine}");
        }
    }
}

