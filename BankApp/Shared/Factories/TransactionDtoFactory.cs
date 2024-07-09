namespace BankApp.Shared.Factories
{
	public class TransactionDtoFactory : Factory<TransactionDto>
	{
        public static TransactionDto Create(string type)
        {
            switch (type)
            {
                case "ByEmail":
                    return new TransactionByEmailDto();
                case "ByWalletCode":
                    return new TransactionByWalletCodeDto();
                default:
                    return new TransactionDto();
            }
        }
    }
}

