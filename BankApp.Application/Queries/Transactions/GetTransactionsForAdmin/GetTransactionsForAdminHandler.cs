using BankApp.Domain.Models;
using BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Application.Queries.Transactions.GetTransactionsForAdmin
{
	public class GetTransactionsForAdminHandler : IRequestHandler<GetTransactionsForAdminQuery, List<Transaction>>
	{
        private readonly BankAppDbContext context;

        public GetTransactionsForAdminHandler(BankAppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<List<Transaction>> Handle(GetTransactionsForAdminQuery request, CancellationToken cancellationToken)
        {
            var transactions = await context.Transactions
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return transactions;
        }
    }
}

