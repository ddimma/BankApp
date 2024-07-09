namespace BankApp.Shared
{
	public class WalletDto
	{
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Id { get; set; }
        public string WalletCode { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsMainWallet { get; set; }
    }
}

