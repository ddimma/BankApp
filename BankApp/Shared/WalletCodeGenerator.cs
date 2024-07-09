namespace BankApp.Shared
{
	public static class WalletCodeGenerator
	{
        public static string GenerateWalletCode()
        {
            Random random = new Random();
            string walletCode = new string(Enumerable.Range(0, 16).Select(_ => random.Next(10).ToString()[0]).ToArray());
            return walletCode;
        }
    }
}

