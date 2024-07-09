using BankApp.Domain.Dtos;
using BankApp.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Application.Queries.UserAccount.GetUsers
{
	public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<User>>
	{
        private readonly UserManager<User> userManager;

        public GetUsersHandler(UserManager<User> userManager)
        {
            ArgumentNullException.ThrowIfNull(userManager);
            this.userManager = userManager;
        }

        public async Task<List<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var adminUsers = await userManager.GetUsersInRoleAsync("Admin");

                return userManager.Users
                    .Where(u => !adminUsers.Contains(u))
                    .ToList();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new();
            }

        }
    }
}

