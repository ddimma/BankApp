using BankApp.Domain.Models;
using MediatR;
namespace BankApp.Application.Queries.Transactions.GetTransactionsForAdmin
{
	public class GetTransactionsForAdminQuery : IRequest<List<Transaction>>
	{
	}
}

