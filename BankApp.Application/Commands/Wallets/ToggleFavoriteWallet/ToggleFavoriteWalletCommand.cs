using MediatR;
namespace BankApp.Application.Commands.ToggleFavoriteWallet
{
	public class ToggleFavoriteWalletCommand : IRequest<CommandsStatus>
	{
		public string Id { get; set; }
	}
}

