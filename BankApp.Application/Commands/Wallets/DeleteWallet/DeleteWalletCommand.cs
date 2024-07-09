using MediatR;
namespace BankApp.Application.Commands.Wallets.DeleteWallet
{
	public class DeleteWalletCommand : IRequest<CommandsStatus>
	{
		public string Id { get; set; }
	}
}

