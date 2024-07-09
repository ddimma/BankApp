using BankApp.Domain.Models;
using BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Application.Queries.Wallets.GetWalletById
{
	public class GetWalletByIdHandler : IRequestHandler<GetWalletByIdQuery, Wallet>
    {
        private readonly BankAppDbContext context;

        public GetWalletByIdHandler(BankAppDbContext context, IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<Wallet> Handle(GetWalletByIdQuery request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.Id, out Guid walletId);
            var wallet = await context.Wallets
                .Where(c => c.Id == walletId)
                .FirstOrDefaultAsync(cancellationToken);

            return wallet;
        }
    }
}

