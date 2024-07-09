using BankApp.Domain.Models;
using MediatR;
namespace BankApp.Application.Queries.GetTransactions
{
	public class GetTransactionsQuery : IRequest<List<Transaction>>
	{
		public string User { get; set; }
	}
}

