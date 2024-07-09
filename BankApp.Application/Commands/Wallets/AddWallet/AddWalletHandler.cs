using BankApp.Domain.Models;
using BankApp.Infrastructure.Persistence;
using BankApp.Shared;
using MediatR;
namespace BankApp.Application.Commands.Wallets.AddWallet
{
    public class AddWalletHandler : IRequestHandler<AddWalletCommand, CommandsStatus>
    {
        private readonly BankAppDbContext context;

        public AddWalletHandler(BankAppDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            this.context = context;
        }

        public async Task<CommandsStatus> Handle(AddWalletCommand request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.UserId, out Guid userId);
            Guid.TryParse(request.Id, out Guid walletId);
            bool isFirstUserWallet = !context.Wallets.Any(w => w.UserId == userId && w.Id != walletId);
            var existingCurrency = context.Currencies.SingleOrDefault(c => c.CurrencyCode == request.Currency);
            var wallet = new Wallet()
            {
                Type = request.Type,
                Amount = request.Amount,
                //Currency = existingCurrency,
                UserId = userId,
                WalletCode = WalletCodeGenerator.GenerateWalletCode(),
                CurrencyId = existingCurrency.Id
            };

            if (isFirstUserWallet)
            {
                wallet.IsMainWallet = true;
            }

            try
            {
                if (wallet.CurrencyId == existingCurrency.Id)
                {
                    await context.Wallets.AddAsync(wallet, cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            return new CommandsStatus();
        }
    }
}

