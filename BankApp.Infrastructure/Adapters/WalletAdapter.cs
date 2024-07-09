using System;
using BankApp.Domain.Models;
using BankApp.Shared;

namespace BankApp.Infrastructure.Adapters
{
	public class WalletAdapter : IAdapter<WalletDto, Wallet>
    {
        public WalletDto ToDto(Wallet source)
        {
            return new WalletDto()
            {
                Type = source.Type,
                Currency = source.Currency.CurrencyCode,
                Amount = source.Amount,
                Id = source.Id.ToString(),
                WalletCode = source.WalletCode,
                IsFavorite = source.IsFavorite,
                IsMainWallet = source.IsMainWallet
            };
        }

        public Wallet ToEntity(WalletDto source)
        {
            return new Wallet()
            {
                Type = source.Type,
                Amount = source.Amount,
                WalletCode = source.WalletCode,
                IsFavorite = source.IsFavorite,
                IsMainWallet = source.IsMainWallet
            };
        }
    }
}

