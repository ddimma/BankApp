using BankApp.Domain.Models;
using BankApp.Infrastructure.ConcreteFactories;
using BankApp.Infrastructure.Factories;
using BankApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BankApp.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var usePostgres = configuration.GetValue<bool>("UsePostgres");

            services.AddSingleton<IDbContextFactory>(provider =>
            {
                return usePostgres ? (IDbContextFactory)new PostgreSqlDbContextFactory() : new SqlServerDbContextFactory();
            });

            services.AddDbContext<BankAppDbContext>(options =>
            {
                if (usePostgres)
                {
                    options.UseNpgsql(configuration.GetConnectionString("PostgreSqlConnection"), x => x.MigrationsAssembly("BankApp.Infrastructure"));
                }
                else
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("BankApp.Infrastructure"));
                }

                options
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                    .EnableSensitiveDataLogging();
            });

            services
                .AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<BankAppDbContext>()
                .AddRoleStore<RoleStore<IdentityRole<Guid>, BankAppDbContext, Guid>>()
                .AddUserStore<UserStore<User, IdentityRole<Guid>, BankAppDbContext, Guid>>()
                .AddUserManager<UserManager<User>>()
                .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
                .AddSignInManager<SignInManager<User>>()
                .AddDefaultTokenProviders();


            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 3;
                options.User.RequireUniqueEmail = false;
                options.Password.RequiredUniqueChars = 1;
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            });

            return services;
        }
    }
}

