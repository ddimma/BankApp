using System;
namespace BankApp.Application.Services
{
    public interface IGenericService<TEntity>
    {
        Task<TEntity> GetEntityAsync(Guid id);
        Task<IEnumerable<TEntity>> GetEntitiesAsync();
        Task DeleteEntity(Guid id);
    }
}

