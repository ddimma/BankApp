using BankApp.Application.Services.CommisionService;
using BankApp.Application.Services.TransactionObserver;
using BankApp.Infrastructure.Persistence;
using BankApp.Infrastructure.WalletMemento;
using BankApp.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Application.Commands.Transactions.AddTransactionByEmail
{
    public class AddTransactionByEmailHandler : IRequestHandler<AddTransactionByEmailCommand, CommandsStatus>
    {
        private readonly CommisionCalculatorService _commissionCalculator;
        private readonly BankAppDbContext context;

        private readonly TransactionProcessor _transactionProcessor;
        private ITransactionObserver _logger;

        public AddTransactionByEmailHandler(BankAppDbContext context,
            CommisionCalculatorService commissionCalculatorService,
            TransactionProcessor transactionProcessor,
            ITransactionObserver logger)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(commissionCalculatorService);
            ArgumentNullException.ThrowIfNull(transactionProcessor);
            ArgumentNullException.ThrowIfNull(logger);
            _commissionCalculator = commissionCalculatorService;
            this.context = context;
            _transactionProcessor = transactionProcessor;
            _logger = logger;
        }

        public async Task<CommandsStatus> Handle(AddTransactionByEmailCommand request, CancellationToken cancellationToken)
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
            .FirstOrDefaultAsync(w => w.User.Email == request.DestinationEmail || w.Id == destination);

            var loggerName = "Console Logger 1";
            _logger = new ConsoleTransactionLogger(loggerName, true);
            _transactionProcessor.Subscribe(_logger);

            WalletExtension walletExtension;

            if (sourceWallet is WalletExtension wallet)
            {
                walletExtension = wallet;
            }
            else
            {
                walletExtension = new WalletExtension
                {
                    Amount = sourceWallet.Amount,
                    Type = sourceWallet.Type,
                    WalletCode = sourceWallet.WalletCode,
                    IsFavorite = sourceWallet.IsFavorite,
                    IsMainWallet = sourceWallet.IsMainWallet
                };
            }

            WalletCaretaker walletCaretaker = new WalletCaretaker(walletExtension);
            walletCaretaker.Backup();

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
                walletCaretaker.Undo();
                return CommandsStatus.Failed("0 Not enough founds.");
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

                    context.Wallets.Update(sourceWallet);
                    context.Wallets.Update(destinationWallet);

                    await context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    _transactionProcessor.ProcessTransaction(newTransaction.Id.ToString(), newTransaction.TransactionAmount, newTransaction.Message);

                    return new CommandsStatus();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    await transaction.RollbackAsync();
                    
                    walletCaretaker.Undo();

                    return CommandsStatus.Failed("0 An error occured.");
                }
            }
        }
    }
}

