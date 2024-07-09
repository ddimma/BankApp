namespace BankApp.Server.Common.JWTToken
{
	public class TokenSettings
	{
        public string SecretKey { get; set; } = null!;
        public int ExpirationInMinutes { get; set; }
    }
}

