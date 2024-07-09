namespace BankApp.Application.Services.TransactionObserver
{
	public class LoggerState
	{
        public string Name { get; private set; }
        public bool Enabled { get; private set; }

        public LoggerState(string name, bool enabled)
        {
            Name = name;
            Enabled = enabled;
        }
    }
}

