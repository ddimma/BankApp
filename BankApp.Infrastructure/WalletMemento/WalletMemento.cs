namespace BankApp.Infrastructure.WalletMemento
{
	public class WalletMemento
	{
        public decimal Amount { get; private set; }
        public string Type { get; private set; }
        public string WalletCode { get; private set; }
        public bool IsFavorite { get; private set; }
        public bool IsMainWallet { get; private set; }

        public WalletMemento(decimal amount,
            string type,
            string walletCode,
            bool isFavorite,
            bool isMainWallet)
        {
            Amount = amount;
            Type = type;
            WalletCode = walletCode;
            IsFavorite = isFavorite;
            IsMainWallet = isMainWallet;
        }
    }
}

