using BankApp.Domain.Common;

namespace BankApp.Domain.Models
{
    public class Commision : BaseEntity
    {
        public string WalletType { get; set; }
        public decimal CommisionRate { get; set; }
    }
}

