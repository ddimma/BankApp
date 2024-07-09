using MediatR;

namespace BankApp.Application.Commands.UserAccount.LoginUser
{
	public class LoginUserCommand : IRequest<CommandsStatus>
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}

