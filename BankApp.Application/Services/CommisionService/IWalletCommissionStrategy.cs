namespace BankApp.Application.Services.CommisionService
{
    public interface IWalletCommissionStrategy
    {
        decimal CalculateCommission(decimal amount);
    }
}

