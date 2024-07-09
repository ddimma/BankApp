using MediatR;
using BankApp.Domain.Models;
using BankApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Application.Queries.GetCommisions
{
	public class GetCommisionsHandler : IRequestHandler<GetCommisionsQuery, IEnumerable<Commision>>
	{
        private readonly BankAppDbContext context;

        public GetCommisionsHandler(BankAppDbContext context)
		{
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<IEnumerable<Commision>> Handle(GetCommisionsQuery request, CancellationToken cancellationToken)
        {
            var commisions = await context.Commisions.AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);

            return commisions;
        }
    }
}

