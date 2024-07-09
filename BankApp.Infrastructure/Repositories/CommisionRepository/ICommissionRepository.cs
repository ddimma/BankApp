namespace BankApp.Infrastructure.Repositories.CommisionRepository
{
	public interface ICommissionRepository
	{
        decimal GetCommissionRate(string walletType);
    }
}

