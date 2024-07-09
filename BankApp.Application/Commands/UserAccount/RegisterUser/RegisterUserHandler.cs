using BankApp.Domain.Enums;
using BankApp.Domain.Models;
using BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Application.Commands.UserAccount.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, CommandsStatus>
    {
        private readonly BankAppDbContext context;
        private readonly UserManager<User> userManager;

        public RegisterUserHandler(BankAppDbContext context, UserManager<User> userManager)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(userManager);

            this.context = context;
            this.userManager = userManager;
        }

        public async Task<CommandsStatus> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var notFirstUser = await context.Users.AnyAsync(cancellationToken);
            var userExists = await context.Users.AnyAsync(user => user.Email == request.Email || user.UserName == request.Username, cancellationToken);

            if (userExists)
                return CommandsStatus.Failed($"The user {request.Username} already exists");

            var user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };

            var createResult = await userManager.CreateAsync(user, request.Password);

            IdentityResult roleResult = new();
            try
            {
                if (notFirstUser)
                    roleResult = await userManager.AddToRoleAsync(user, "User");
                else
                    roleResult = await userManager.AddToRoleAsync(user, "Admin");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            if (!roleResult.Succeeded || !createResult.Succeeded)
                return CommandsStatus.Failed("Failed to register the user");

            return new CommandsStatus();
        }
    }
}

