using BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Application.Commands.Transactions.DeleteTransaction
{
	public class DeleteTransactionHandler : IRequestHandler<DeleteTransactionCommand, CommandsStatus>
	{
        private readonly BankAppDbContext context;

        public DeleteTransactionHandler(BankAppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<CommandsStatus> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.Id, out Guid currencyId);

            var transaction = await context.Transactions
                .FirstOrDefaultAsync(x => x.Id == currencyId, cancellationToken);

            if (transaction == null)
            {
                return CommandsStatus.Failed("Currency not found.");
            }

            try
            {
                context.Transactions.Remove(transaction);
                return await context.SaveChangesAsync() > 0 ? new CommandsStatus() : CommandsStatus.Failed($"Failed to delete transaction{transaction.Id.ToString()}");
            }
            catch (Exception ex)
            {
                return CommandsStatus.Failed(ex.Message);
            }
        }
    }
}

