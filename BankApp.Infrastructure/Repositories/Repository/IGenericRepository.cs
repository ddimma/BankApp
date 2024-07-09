namespace BankApp.Infrastructure.Repositories.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task DeleteAsync(Guid id);
    }
}

