using BankApp.Domain.Models;
using MediatR;
namespace BankApp.Application.Queries.Wallets.GetWalletById
{
	public class GetWalletByIdQuery : IRequest<Wallet>
    {
        public string Id { get; set; }
    }
}

