using System;
namespace BankApp.Shared
{
	public class CurrencyDto
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public string CurrencyCode { get; set; }
        public decimal ChangeRate { get; set; }
        public bool CanBeRemoved { get; set; } = true;
    }
}

