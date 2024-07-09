namespace BankApp.Application.Services.TransactionObserver
{
    public class ConsoleTransactionLogger : ITransactionObserver
    {
        private readonly LoggerState _state;

        public ConsoleTransactionLogger() { }

        public ConsoleTransactionLogger(string name, bool enabled)
        {
            _state = new LoggerState(name, enabled);
        }

        public LoggerState State => _state;

        public void LogTransaction(string userName, decimal amount, string transactionType)
        {
            Console.WriteLine($"Transaction: User '{userName}' made a '{transactionType}' transaction of ${amount}");
        }
    }
}

