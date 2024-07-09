using System;
using BankApp.Infrastructure.Repositories.CommisionRepository;

namespace BankApp.Application.Services.CommisionService.Strategies
{
	public class GoldWalletCommissionStrategy : IWalletCommissionStrategy
	{
        private readonly ICommissionRepository _commissionRepository;

        public GoldWalletCommissionStrategy(ICommissionRepository commissionRepository)
		{
            ArgumentNullException.ThrowIfNull(commissionRepository);
            _commissionRepository = commissionRepository;
		}

        public decimal CalculateCommission(decimal amount)
        {
            var commissionRate = _commissionRepository.GetCommissionRate("Gold");
            return amount * commissionRate;
        }
    }
}

