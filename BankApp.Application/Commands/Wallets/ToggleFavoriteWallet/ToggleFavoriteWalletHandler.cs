using BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Application.Commands.ToggleFavoriteWallet
{
	public class ToggleFavoriteWalletHandler : IRequestHandler <ToggleFavoriteWalletCommand, CommandsStatus>
	{
        private readonly BankAppDbContext context;
        public ToggleFavoriteWalletHandler(BankAppDbContext context)
		{
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<CommandsStatus> Handle(ToggleFavoriteWalletCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Guid.TryParse(request.Id, out Guid id);
                var existingWallet = await context.Wallets.FindAsync(id);

                if (existingWallet == null)
                {
                    return CommandsStatus.Failed("Wallet not found.");
                }

                existingWallet.IsFavorite = !existingWallet.IsFavorite;

                if (context.Entry(existingWallet).State == EntityState.Detached)
                {
                    context.Wallets.Attach(existingWallet);
                    context.Entry(existingWallet).State = EntityState.Modified;
                }

                await context.SaveChangesAsync(cancellationToken);

                return new CommandsStatus();
            }
            catch (Exception ex)
            {
                return CommandsStatus.Failed(ex.Message);
            }
            
        }
    }
}

