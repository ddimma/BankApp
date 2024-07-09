using BankApp.Domain.Models;
using BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Application.Commands.AddCommision
{
	public class AddCommisionHandler : IRequestHandler <AddCommisionCommand, CommandsStatus>
	{
        private readonly BankAppDbContext context;
        public AddCommisionHandler(BankAppDbContext context)
		{
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<CommandsStatus> Handle(AddCommisionCommand request, CancellationToken cancellationToken)
        {
            if (await context.Commisions.AnyAsync(x => x.WalletType == request.WalletType, default))
                return CommandsStatus.Failed("0 Wallet de asa tip deja exista.");

            var commision = new Commision()
            {
                WalletType = request.WalletType,
                CommisionRate = request.CommisionRate
            };

            await context.Commisions.AddAsync(commision, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return new CommandsStatus();
        }
    }
}

