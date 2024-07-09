using BankApp.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;

namespace BankApp.Infrastructure.Factories
{
	public interface IDbContextFactory
	{
        BankAppDbContext CreateDbContext(IConfiguration configuration);
    }
}

