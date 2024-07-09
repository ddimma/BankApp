using BankApp.Domain.Models;
using MediatR;
namespace BankApp.Application.Queries.GetCommisions
{
	public class GetCommisionsQuery : IRequest<IEnumerable<Commision>>
	{
	}
}

