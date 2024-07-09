using BankApp.Infrastructure.Factories;
using BankApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BankApp.Infrastructure.ConcreteFactories
{
	public class PostgreSqlDbContextFactory : IDbContextFactory
	{
        public BankAppDbContext CreateDbContext(IConfiguration configuration)
        {
            return new BankAppDbContext(new DbContextOptionsBuilder<BankAppDbContext>()
            .UseNpgsql(configuration["ConnectionStrings:PostgreSqlConnection"], x => x.MigrationsAssembly("BankApp.Infrastructure"))
            .Options);
        }
    }
}

