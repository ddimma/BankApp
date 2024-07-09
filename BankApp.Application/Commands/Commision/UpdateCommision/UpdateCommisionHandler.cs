using BankApp.Infrastructure.Persistence;
using MediatR;

namespace BankApp.Application.Commands.UpdateCommision
{
	public class UpdateCommisionHandler : IRequestHandler<UpdateCommisionCommand, CommandsStatus>
	{
        private readonly BankAppDbContext context;
		public UpdateCommisionHandler(BankAppDbContext context)
		{
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<CommandsStatus> Handle(UpdateCommisionCommand request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.CommisionId, out Guid commisionId);
            var existingCommision = await context.Commisions.FindAsync(commisionId);

            if (existingCommision == null)
            {
                return CommandsStatus.Failed("Such a commision not found.");
            }
            existingCommision.CommisionRate = request.CommisionRate;
            existingCommision.WalletType = request.WalletType;

            await context.SaveChangesAsync(cancellationToken);

            return new CommandsStatus();
        }
    }
}

