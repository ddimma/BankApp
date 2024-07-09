namespace BankApp.Application.Services.WalletStates
{
    public interface IWalletState
    {
        decimal Amount { get; }
        string CurrencyCode { get; }
        decimal ConvertAmountTo(string targetCurrencyCode);
    }
}

