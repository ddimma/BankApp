namespace BankApp.Application.Services.LoggingServices
{
	public class LoggingService
	{
        private readonly ILogger _logger;

        public LoggingService(ILogger logger)
        {
            _logger = logger;
        }

        public void Info(string message)
        {
            _logger.Log(message, LogLevel.Info);
        }

        public void Warning(string message)
        {
            _logger.Log(message, LogLevel.Warning);
        }

        public void Error(string message)
        {
            _logger.Log(message, LogLevel.Error);
        }
    }
}

