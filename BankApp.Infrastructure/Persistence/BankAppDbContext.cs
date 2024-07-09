using BankApp.Domain.Models;
using BankApp.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Infrastructure.Persistence
{
	public class BankAppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
	{
		public BankAppDbContext(DbContextOptions<BankAppDbContext> options) :  base(options)
		{
		}

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Commision> Commisions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>().HasKey(x => x.Id);
            modelBuilder.Entity<Transaction>().HasKey(x => x.Id);
            modelBuilder.Entity<Currency>().HasKey(x => x.Id);
            modelBuilder.Entity<Commision>().HasKey(x => x.Id);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.Wallets)
                .WithOne(e => e.Currency)
                .HasForeignKey(e => e.CurrencyId)
                .IsRequired();

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.SourceWallet)
                .WithMany()
                .HasForeignKey(t => t.SourceWalletId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.DestinationWallet)
                .WithMany()
                .HasForeignKey(t => t.DestinationWalletId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            modelBuilder.Entity<Wallet>()
            .HasOne(w => w.User)
            .WithMany(u => u.Wallets)
            .HasForeignKey(w => w.UserId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfigurations());
        }
    }
}

