using BankApp.Domain.Models;
using BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Application.Queries.GetCurrencies
{
    public class GetCurrenciesHandler : IRequestHandler<GetCurrenciesQuery, List<Currency>>
    {
        private readonly BankAppDbContext context;


        public GetCurrenciesHandler(BankAppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<List<Currency>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
        {
            var currencies = await context.Currencies.AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);

            return currencies;
        }
    }
}

