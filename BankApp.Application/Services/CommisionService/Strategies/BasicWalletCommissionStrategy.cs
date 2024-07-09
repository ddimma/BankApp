using BankApp.Infrastructure.Repositories.CommisionRepository;

namespace BankApp.Application.Services.CommisionService.Strategies
{
    public class BasicWalletCommissionStrategy : IWalletCommissionStrategy
    {
        private readonly ICommissionRepository _commissionRepository;

        public BasicWalletCommissionStrategy(ICommissionRepository commissionRepository)
        {
            ArgumentNullException.ThrowIfNull(commissionRepository);
            _commissionRepository = commissionRepository;
        }

        public decimal CalculateCommission(decimal amount)
        {
            var commissionRate = _commissionRepository.GetCommissionRate("Basic");
            return amount * commissionRate;
        }
    }
}

