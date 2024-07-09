using MediatR;
namespace BankApp.Application.Commands.Transactions.DeleteTransaction
{
	public class DeleteTransactionCommand : IRequest<CommandsStatus>
	{
		public string Id { get; set; }
	}
}

