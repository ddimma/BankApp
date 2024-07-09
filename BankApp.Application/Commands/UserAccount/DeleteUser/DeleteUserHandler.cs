using MediatR;
using BankApp.Domain.Models;
using BankApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Application.Commands.UserAccount.DeleteUser
{
	public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, CommandsStatus>
	{
        private readonly BankAppDbContext context;
        private readonly UserManager<User> userManager;

        public DeleteUserHandler(BankAppDbContext context, UserManager<User> userManager)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(userManager);

            this.context = context;
            this.userManager = userManager;
        }

        public async Task<CommandsStatus> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.Id, out Guid userId);
            var userToDelete = await context.Users.SingleOrDefaultAsync(user => user.Id == userId, cancellationToken);

            if (userToDelete is null)
                return CommandsStatus.Failed("User not found.");

            try
            {
                var result = await userManager.DeleteAsync(userToDelete);
                if (result.Succeeded)
                {
                    return new CommandsStatus();
                }
                return CommandsStatus.Failed("Error deleting the user");
            }
            catch (Exception ex)
            {
                return CommandsStatus.Failed($"Error deleting the user: {ex.Message}");
            }
        }
    }
}

