using BankApp.Domain.Models;
using BankApp.Infrastructure.Persistence;

namespace BankApp.Infrastructure.Repositories.CommisionRepository
{
    public class CommissionRepository : ICommissionRepository
    {
        private readonly BankAppDbContext _context;

        public CommissionRepository(BankAppDbContext context)
        {
            _context = context;
        }

        public decimal GetCommissionRate(string walletType)
        {
            var commission = _context.Set<Commision>()
                .FirstOrDefault(c => c.WalletType == walletType);

            if (commission == null)
            {
                throw new Exception($"Commission rate not found for wallet type: {walletType}");
            }

            return commission?.CommisionRate ?? 0;
        }
    }
}

