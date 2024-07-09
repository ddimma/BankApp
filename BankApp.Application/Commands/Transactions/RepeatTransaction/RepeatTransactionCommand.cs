using MediatR;
namespace BankApp.Application.Commands.Transactions.RepeatTransaction
{
	public class RepeatTransactionCommand : IRequest<CommandsStatus>
	{
        public string Type { get; set; }
        public string Message { get; set; }
        public string SourceWalletId { get; set; }
        public string DestinationWalletId { get; set; }
        public decimal TransactionAmount { get; set; }
    }
}

