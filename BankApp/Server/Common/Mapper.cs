using BankApp.Domain.Models;
using BankApp.Shared;

namespace BankApp.Server.Common
{
    public static class Mapper
    {
        public static IEnumerable<CurrencyDto> Map(IEnumerable<Currency> currencies)
        {
            var dtos = new List<CurrencyDto>();

            foreach (var currency in currencies)
            {
                var dto = new CurrencyDto()
                {
                    Id = currency.Id.ToString(),
                    Name = currency.Name,
                    CurrencyCode = currency.CurrencyCode,
                    ChangeRate = currency.ChangeRate,
                };

                dtos.Add(dto);
            }

            return dtos;
        }

        public static IEnumerable<WalletDto> Map(IEnumerable<Wallet> wallets)
        {
            var dtos = new List<WalletDto>();

            foreach (var wallet in wallets)
            {
                var dto = new WalletDto()
                {
                    Id = wallet.Id.ToString(),
                    Type = wallet.Type,
                    Currency = wallet.Currency.CurrencyCode,
                    Amount = wallet.Amount,
                    WalletCode = wallet.WalletCode,
                    IsFavorite = wallet.IsFavorite
                };

                dtos.Add(dto);
            }

            return dtos;
        }

        public static IEnumerable<TransactionDto> Map(IEnumerable<Transaction> transactions)
        {
            var dtos = new List<TransactionDto>();

            foreach (var transaction in transactions)
            {
                var dto = new TransactionDto()
                {
                    SourceWalletId = transaction.SourceWalletId.ToString(),
                    DestinationWalletId = transaction.DestinationWalletId.ToString(),
                    TransactionAmount = transaction.TransactionAmount,
                    Message = transaction.Message,
                };

                dtos.Add(dto);
            }

            return dtos;
        }
    }
}

