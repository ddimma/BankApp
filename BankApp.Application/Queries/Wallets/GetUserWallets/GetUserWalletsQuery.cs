using BankApp.Domain.Models;
using MediatR;
namespace BankApp.Application.Queries.Wallets.GetUserWallets
{
	public class GetUserWalletsQuery : IRequest<IEnumerable<Wallet>>
	{
        public string UserId { get; set; }
    }
}

