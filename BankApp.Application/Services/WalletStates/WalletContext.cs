using System;
using BankApp.Shared;

namespace BankApp.Application.Services.WalletStates
{
	public class WalletContext
	{
        private IWalletState _state;

        public WalletContext() { }

        public WalletContext(decimal amount, string currencyCode)
        {
            _state = new ActiveWalletState(amount, currencyCode);
        }

        public decimal Amount => _state.Amount;
        public string CurrencyCode => _state.CurrencyCode;

        public async Task ChangeCurrencyAsync(string newCurrencyCode)
        {
            if (_state.GetType() == typeof(ActiveWalletState))
            {
                // Convert amount to new currency and update state
                var convertedAmount = _state.ConvertAmountTo(newCurrencyCode);
                _state = new ActiveWalletState(convertedAmount, newCurrencyCode);
            }
            else
            {
                // Handle error or exception for inactive wallet
                throw new InvalidOperationException("Wallet is inactive and cannot change currency.");
            }
        }
    }
}

