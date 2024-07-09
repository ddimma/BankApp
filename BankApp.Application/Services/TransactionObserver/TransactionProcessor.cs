namespace BankApp.Application.Services.TransactionObserver
{
    public class TransactionProcessor
    {
        private readonly List<ITransactionObserver> _loggers = new List<ITransactionObserver>();

        public void Subscribe(ITransactionObserver logger)
        {
            _loggers.Add(logger);
        }

        public void Unsubscribe(ITransactionObserver logger)
        {
            _loggers.Remove(logger);
        }

        public void ProcessTransaction(string userName, decimal amount, string transactionType)
        {
            foreach (var logger in _loggers)
            {
                logger.LogTransaction(userName, amount, transactionType);
            }
        }

        public List<LoggerState> GetState()
        {
            return _loggers.Select(logger => logger.State).ToList();
        }
    }
}

