using BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Application.Commands.Wallets.DeleteWallet
{
	public class DeleteWalletHandler : IRequestHandler<DeleteWalletCommand, CommandsStatus>
	{

        private readonly BankAppDbContext context;

        public DeleteWalletHandler(BankAppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<CommandsStatus> Handle(DeleteWalletCommand request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.Id, out Guid walletId);

            var wallet = await context.Wallets
                .FirstOrDefaultAsync(x => x.Id == walletId, cancellationToken);

            if (wallet == null)
            {
                return CommandsStatus.Failed("Wallet not found.");
            }

            try
            {
                context.Wallets.Remove(wallet);
                return await context.SaveChangesAsync() > 0 ? new CommandsStatus() : CommandsStatus.Failed("Failed to update the wallet");
            }
            catch (Exception ex)
            {
                return CommandsStatus.Failed(ex.Message);
            }
        }
    }
}

