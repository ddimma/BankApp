using BankApp.Domain.Dtos;
using MediatR;

namespace BankApp.Application.Queries.UserAccount.GetUserDetails
{
    public class GetUserDetailsQuery : IRequest<UserDetails>
    {
        public string Username { get; set; }
    }
}

