using BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Application.Commands.UpdateCurrency
{
    public class UpdateCurrencyHandler : IRequestHandler<UpdateCurrencyCommand, CommandsStatus>
    {
        private readonly BankAppDbContext context;

        public UpdateCurrencyHandler(BankAppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<CommandsStatus> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.Id, out Guid id))
            {
                return CommandsStatus.Failed("Invalid currency ID.");
            }

            var existingCurrency = await context.Currencies.FindAsync(id);

            if (existingCurrency == null)
            {
                return CommandsStatus.Failed("No currency found.");
            }

            existingCurrency.Name = request.Name;
            existingCurrency.CurrencyCode = request.CurrencyCode;
            existingCurrency.ChangeRate = request.ChangeRate;

            if (context.Entry(existingCurrency).State == EntityState.Detached)
            {
                context.Currencies.Attach(existingCurrency);
                context.Entry(existingCurrency).State = EntityState.Modified;
            }
            try
            {
                await context.SaveChangesAsync(cancellationToken);

                // Check entity state after saving
                var entityStateAfterSave = context.Entry(existingCurrency).State;
                Console.WriteLine($"Entity State After Save: {entityStateAfterSave}");

                return new();
            }
            catch (Exception ex)
            {
                return CommandsStatus.Failed(ex.Message);
            }
        }
    }
}

