using MediatR;

namespace BankApp.Application.Commands.UserAccount.RegisterUser
{
    public class RegisterUserCommand : IRequest<CommandsStatus>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

