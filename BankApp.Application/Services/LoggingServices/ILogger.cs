namespace BankApp.Application.Services.LoggingServices
{
	public interface ILogger
	{
        void Log(string message, LogLevel level);
    }
}

