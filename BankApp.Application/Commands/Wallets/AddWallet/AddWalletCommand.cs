using MediatR;
namespace BankApp.Application.Commands.Wallets.AddWallet
{
	public class AddWalletCommand : IRequest<CommandsStatus>
	{
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Id { get; set; }
        public string UserId { get; set; }
        public bool IsFavorite { get; set; }
        public string WalletCode { get; set; }
    }
}

