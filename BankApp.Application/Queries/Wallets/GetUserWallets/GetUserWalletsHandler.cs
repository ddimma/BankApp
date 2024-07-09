using BankApp.Domain.Models;
using BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Application.Queries.Wallets.GetUserWallets
{
	public class GetUserWalletsHandler : IRequestHandler<GetUserWalletsQuery, IEnumerable<Wallet>>
    {
        private readonly BankAppDbContext context;


        public GetUserWalletsHandler(BankAppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<IEnumerable<Wallet>> Handle(GetUserWalletsQuery request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.UserId, out Guid userId);
            var wallets = await context.Wallets
                .Where(w => w.UserId == userId)
                .Include(x => x.Currency).AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);

            return wallets;

        }
    }
}

