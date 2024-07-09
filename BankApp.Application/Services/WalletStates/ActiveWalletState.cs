using System;
using BankApp.Shared;

namespace BankApp.Application.Services.WalletStates
{
    public class ActiveWalletState : IWalletState
    {
        private readonly decimal _amount;
        private readonly string _currencyCode;

        public ActiveWalletState(decimal amount, string currencyCode)
        {
            _amount = amount;
            _currencyCode = currencyCode;
        }

        public decimal Amount => _amount;
        public string CurrencyCode => _currencyCode;

        public decimal ConvertAmountTo(string targetCurrencyCode)
        {
            return Converter.ConvertCurrency(_amount, _currencyCode, targetCurrencyCode);
        }
    }
}

