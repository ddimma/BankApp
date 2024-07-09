using BankApp.Application.Services.CommisionService.Strategies;
using BankApp.Infrastructure.Repositories.CommisionRepository;

namespace BankApp.Application.Services.CommisionService
{
	public class CommisionCalculatorService
	{
        private IWalletCommissionStrategy _commissionStrategy;
        private readonly ICommissionRepository _commissionRepository;

        public CommisionCalculatorService(ICommissionRepository commissionRepository)
        {
            ArgumentNullException.ThrowIfNull(commissionRepository);
            _commissionRepository = commissionRepository;
        }

        public void InitializeCommissionStrategy(string walletType)
        {
            _commissionStrategy = GetCommissionStrategy(walletType);
        }

        private IWalletCommissionStrategy GetCommissionStrategy(string walletType)
        {
            switch (walletType.ToLower())
            {
                case "basic":
                    return new BasicWalletCommissionStrategy(_commissionRepository);
                case "silver":
                    return new SilverWalletCommissionStrategy(_commissionRepository);
                case "gold":
                    return new GoldWalletCommissionStrategy(_commissionRepository);
                case "platinum":
                    return new PlatinumWalletCommissionStrategy(_commissionRepository);

                default:
                    throw new ArgumentOutOfRangeException(nameof(walletType));
            }
        }

        public decimal CalculateCommission(decimal amount)
        {
            return _commissionStrategy.CalculateCommission(amount);
        }
    }
}

