using MediatR;

namespace BankApp.Application.Commands.AddCurrency
{
	public class AddCurrencyCommand : IRequest<CommandsStatus>
	{
        public decimal ChangeRate { get; set; }
        public string Name { get; set; }
        public string CurrencyCode { get; set; }
    }
}

