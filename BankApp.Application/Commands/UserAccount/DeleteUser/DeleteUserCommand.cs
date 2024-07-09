using MediatR;

namespace BankApp.Application.Commands.UserAccount.DeleteUser
{
	public class DeleteUserCommand : IRequest<CommandsStatus>
    {
		public string Id { get; set; }
	}
}

