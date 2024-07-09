using BankApp.Infrastructure.Factories;
using BankApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BankApp.Infrastructure.ConcreteFactories
{
	public class SqlServerDbContextFactory : IDbContextFactory
	{
        public BankAppDbContext CreateDbContext(IConfiguration configuration)
        {
            return new BankAppDbContext(new DbContextOptionsBuilder<BankAppDbContext>()
            .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"], x => x.MigrationsAssembly("BankApp.Infrastructure"))
            .Options) ;
        }
    }
}

