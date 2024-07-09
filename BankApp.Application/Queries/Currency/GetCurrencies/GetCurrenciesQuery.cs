using BankApp.Domain.Models;
using MediatR;

namespace BankApp.Application.Queries.GetCurrencies
{
	public class GetCurrenciesQuery : IRequest<List<Currency>>
    {
    
	}
}

