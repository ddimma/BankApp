using BankApp.Domain.Models;
using MediatR;

namespace BankApp.Application.Queries.GetCurrencyById
{
	public class GetCurrencyByIdQuery : IRequest<Currency>
    {
		public string Id { get; set; }
	}
}

