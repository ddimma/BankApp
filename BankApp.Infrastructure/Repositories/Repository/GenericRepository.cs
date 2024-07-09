using BankApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Infrastructure.Repositories.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly BankAppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(BankAppDbContext context)
		{
            ArgumentNullException.ThrowIfNull(context);
            _context = context;
            _dbSet = context.Set<TEntity>();
        }


        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> SaveDbChangesAsync()
        {
            return await _context.SaveChangesAsync() != 0;
        }
    }
}

