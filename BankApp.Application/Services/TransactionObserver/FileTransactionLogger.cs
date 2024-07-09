namespace BankApp.Application.Services.TransactionObserver
{
    public class FileTransactionLogger : ITransactionObserver
    {
        private readonly string _filePath;
        private readonly LoggerState _state;


        public FileTransactionLogger(string filePath)
        {
            _filePath = filePath;
        }

        public FileTransactionLogger(string name, bool enabled)
        {
            _state = new LoggerState(name, enabled);
        }

        public LoggerState State => _state;

        public void LogTransaction(string userName, decimal amount, string transactionType)
        {
            using (StreamWriter writer = new StreamWriter(_filePath, true))
            {
                writer.WriteLine($"Transaction: User '{userName}' made a '{transactionType}' transaction of ${amount}");
            }
        }
    }
}

