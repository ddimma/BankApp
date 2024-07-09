using BankApp.Domain.Models;
using MediatR;
namespace BankApp.Application.Queries.UserAccount.GetUsers
{
	public class GetUsersQuery : IRequest<List<User>>
	{
	}
}

