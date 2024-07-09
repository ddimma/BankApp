using BankApp.Application.Services.CommisionService;
using BankApp.Infrastructure.Persistence;
using BankApp.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Application.Commands.Transactions.RepeatTransaction
{
    public class AddTransactionByEmailHandler : IRequestHandler<RepeatTransactionCommand, CommandsStatus>
    {
        private readonly BankAppDbContext context;
        private readonly CommisionCalculatorService _commissionCalculator;

        public AddTransactionByEmailHandler(BankAppDbContext context, CommisionCalculatorService commissionCalculatorService)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(commissionCalculatorService);
            _commissionCalculator = commissionCalculatorService;
            this.context = context;
        }

        public async Task<CommandsStatus> Handle(RepeatTransactionCommand request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.SourceWalletId, out var source);
            Guid.TryParse(request.DestinationWalletId, out var destination);
            var sourceWallet = await context.Wallets
                .Include(w => w.Currency)
                .Include(w => w.User)
                .FirstOrDefaultAsync(w => w.Id == source);

            var destinationWallet = await context.Wallets
            .Include(w => w.Currency)
            .Include(w => w.User)
            .OrderByDescending(w => w.IsMainWallet)
            .FirstOrDefaultAsync(w => w.Id == destination);


            if (sourceWallet == null || destinationWallet == null)
            {
                return CommandsStatus.Failed("0 No sourse or destinaton wallets found.");
            }
            if (request.TransactionAmount < 0)
            {
                return CommandsStatus.Failed("0 Invalid transaction amount.");
            }

            decimal convertedAmount = Converter.ConvertCurrency(request.TransactionAmount, sourceWallet.Currency.CurrencyCode, destinationWallet.Currency.CurrencyCode);
            _commissionCalculator.InitializeCommissionStrategy(sourceWallet.Type);
            var appliedCommision = _commissionCalculator.CalculateCommission(request.TransactionAmount);

            sourceWallet.Amount -= request.TransactionAmount;
            sourceWallet.Amount -= appliedCommision;

            if (sourceWallet.Amount < 0)
            {
                return CommandsStatus.Failed("0 Resurse insuficiente.");
            }

            destinationWallet.Amount += convertedAmount;

            var newTransaction = new Domain.Models.Transaction()
            {
                Message = request.Message,
                SourceWalletId = sourceWallet.Id,
                DestinationWalletId = destinationWallet.Id,
                TransactionAmount = request.TransactionAmount
            };

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    await context.Transactions.AddAsync(newTransaction, cancellationToken);
                    await context.SaveChangesAsync();

                    context.Wallets.Update(sourceWallet);
                    context.Wallets.Update(destinationWallet);

                    await transaction.CommitAsync();

                    return new CommandsStatus();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    await transaction.RollbackAsync();
                    return CommandsStatus.Failed("0 A apătut o eroare.");
                }
            }
        }
    }
}

