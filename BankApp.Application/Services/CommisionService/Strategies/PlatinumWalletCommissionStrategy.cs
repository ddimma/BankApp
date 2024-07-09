using System;
using BankApp.Infrastructure.Repositories.CommisionRepository;

namespace BankApp.Application.Services.CommisionService.Strategies
{
	public class PlatinumWalletCommissionStrategy : IWalletCommissionStrategy
	{
        private readonly ICommissionRepository _commissionRepository;

        public PlatinumWalletCommissionStrategy(ICommissionRepository commissionRepository)
		{
            ArgumentNullException.ThrowIfNull(commissionRepository);
            _commissionRepository = commissionRepository;
		}

        public decimal CalculateCommission(decimal amount)
        {
            var commissionRate = _commissionRepository.GetCommissionRate("Platinum");
            return amount * commissionRate;
        }
    }
}

