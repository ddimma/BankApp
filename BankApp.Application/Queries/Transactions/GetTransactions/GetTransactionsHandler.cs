using BankApp.Application.Queries.GetTransactions;
using BankApp.Domain.Models;
using BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EndavaTechCourse.BankApp.Application.Queries.GetTransactions
{
	public class GetTransactionsHandler : IRequestHandler<GetTransactionsQuery, List<Transaction>>
	{
        private readonly BankAppDbContext context;


        public GetTransactionsHandler(BankAppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<List<Transaction>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.User))
            {
                return new();
            }
            var transactions = await context.Transactions
                .AsNoTracking()
                .Where(t => t.SourceWallet.User.UserName == request.User)
                .ToListAsync(cancellationToken);

            return transactions;
        }
    }
}

