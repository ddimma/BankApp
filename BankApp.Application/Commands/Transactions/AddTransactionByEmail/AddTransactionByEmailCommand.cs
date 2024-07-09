using MediatR;
namespace BankApp.Application.Commands.Transactions.AddTransactionByEmail
{
	public class AddTransactionByEmailCommand : IRequest<CommandsStatus>
	{
        public string Message { get; set; }
        public string SourceWalletId { get; set; }
        public string DestinationWalletId { get; set; }
        public string DestinationEmail { get; set; }
        public decimal TransactionAmount { get; set; }
    }
}

