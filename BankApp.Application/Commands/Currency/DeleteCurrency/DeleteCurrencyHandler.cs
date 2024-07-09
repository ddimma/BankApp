using BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Application.Commands.DeteleCurrency
{
    public class DeleteCurrencyHandler : IRequestHandler<DeleteCurrencyCommand, CommandsStatus>
    {
        private readonly BankAppDbContext context;

        public DeleteCurrencyHandler(BankAppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<CommandsStatus> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.Id, out Guid currencyId);

            var hasWallets = await context.Wallets.AnyAsync(w => w.CurrencyId == currencyId, cancellationToken);

            if (hasWallets)
            {
                return CommandsStatus.Failed("Cannot delete currency. There are associated wallets.");
            }

            var currency = await context.Currencies
                .FirstOrDefaultAsync(x => x.Id == currencyId, cancellationToken);

            if (currency == null)
            {
                return CommandsStatus.Failed("Currency not found.");
            }

            try
            {
                context.Currencies.Remove(currency);
                return await context.SaveChangesAsync() > 0 ? new CommandsStatus() : CommandsStatus.Failed("Failed to update the currency");
            }
            catch (Exception ex)
            {
                return CommandsStatus.Failed(ex.Message);
            }
        }
    }
}

