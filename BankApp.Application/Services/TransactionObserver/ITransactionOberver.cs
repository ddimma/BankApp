namespace BankApp.Application.Services.TransactionObserver
{
	public interface ITransactionObserver
	{
        LoggerState State { get; }

        void LogTransaction(string userName, decimal amount, string transactionType);
    }
}

