using System;
namespace BankApp.Application.Services.WalletStates
{
	public class InactiveWalletState : IWalletState
	{
        public decimal Amount => 0;
        public string CurrencyCode => "N/A";

        public decimal ConvertAmountTo(string targetCurrencyCode)
        {
            throw new InvalidOperationException("Wallet is inactive and cannot perform conversions.");
        }
    }
}

