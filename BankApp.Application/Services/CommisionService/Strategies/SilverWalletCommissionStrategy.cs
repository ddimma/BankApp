using BankApp.Infrastructure.Repositories.CommisionRepository;

namespace BankApp.Application.Services.CommisionService.Strategies
{
	public class SilverWalletCommissionStrategy : IWalletCommissionStrategy
	{
        private readonly ICommissionRepository _commissionRepository;

        public SilverWalletCommissionStrategy(ICommissionRepository commissionRepository)
		{
            ArgumentNullException.ThrowIfNull(commissionRepository);
            _commissionRepository = commissionRepository;
		}

        public decimal CalculateCommission(decimal amount)
        {
            var commissionRate = _commissionRepository.GetCommissionRate("Silver");
            return amount * commissionRate;
        }
    }
}

