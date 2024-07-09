using System;
namespace BankApp.Domain.Models
{
	public class CreditWallet : Wallet
	{
		public decimal CreditLimit { get; set; }
	}
}

