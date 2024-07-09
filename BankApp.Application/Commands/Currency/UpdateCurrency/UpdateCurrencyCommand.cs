using MediatR;

namespace BankApp.Application.Commands.UpdateCurrency
{
	public class UpdateCurrencyCommand : IRequest<CommandsStatus>
    {
		public string Id { get; set; }
        public string Name { get; set; }
        public string CurrencyCode { get; set; }
        public decimal ChangeRate { get; set; }
        public bool CanBeRemoved { get; set; } = true;
    }
}

