using BankApp.Domain.Models;
using BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Application.Queries.GetCurrencyById
{
    public class GetCurrencyByIdHandler : IRequestHandler<GetCurrencyByIdQuery, Currency>
    {
        private readonly BankAppDbContext context;

        public GetCurrencyByIdHandler(BankAppDbContext context, IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<Currency> Handle(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.Id, out Guid currencyId);
            var currency = await context.Currencies
                .Where(c => c.Id == currencyId)
                .FirstOrDefaultAsync(cancellationToken);

            return currency;
        }
    }
}

