using MediatR;
namespace BankApp.Application.Commands.Transactions.AddTransactionByEmail
{
	public class AddTransactionByWalletCodeCommand : IRequest<CommandsStatus>
	{
        public string Message { get; set; }
        public string SourceWalletId { get; set; }
        public string DestinationWalletId { get; set; }
        public string DestinationWalletCode { get; set; }
        public decimal TransactionAmount { get; set; }
    }
}

