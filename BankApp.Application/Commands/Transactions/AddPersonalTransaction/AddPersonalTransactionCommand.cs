using MediatR;
namespace BankApp.Application.Commands.Transactions.AddPersonalTransaction
{
	public class AddPersonalTransactionCommand : IRequest<CommandsStatus>
	{
        public string Message { get; set; }
        public string SourceWalletId { get; set; }
        public string DestinationWalletId { get; set; }
        public decimal TransactionAmount { get; set; }
    }
}

