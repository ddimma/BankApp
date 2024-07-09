using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace BankApp.Application.Services
{
    public class GenericServiceDecorator<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        private readonly IGenericService<TEntity> _decoratedService;
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;

        public GenericServiceDecorator(IGenericService<TEntity> decoratedService, ILogger<GenericServiceDecorator<TEntity>> logger, IMemoryCache cache)
        {
            _decoratedService = decoratedService;
            _logger = logger;
            _cache = cache;
        }

        public async Task DeleteEntity(Guid id)
        {
            _logger.LogInformation($"Attempting to delete {typeof(TEntity).Name} with id {id}.");

            await _decoratedService.DeleteEntity(id);

            _cache.Remove($"Entity-{typeof(TEntity).Name}-{id}");

            _logger.LogInformation($"{typeof(TEntity).Name} with id {id} deleted successfully.");
        }

        public async Task<IEnumerable<TEntity>> GetEntitiesAsync()
        {
            var cacheKey = $"All-{typeof(TEntity).Name}";

            _logger.LogInformation($"Attempting to retrieve all {typeof(TEntity).Name} from cache.");

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<TEntity> entities))
            {
                _logger.LogInformation($"Cache miss for all {typeof(TEntity).Name}. Fetching from decorated service.");
                entities = await _decoratedService.GetEntitiesAsync();

                _cache.Set(cacheKey, entities, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                });
            }
            else
            {
                _logger.LogInformation($"Cache hit for all {typeof(TEntity).Name}.");
            }

            return entities;
        }

        public async Task<TEntity> GetEntityAsync(Guid id)
        {
            var cacheKey     = $"Entity-{typeof(TEntity).Name}-{id}";
            _logger.LogInformation($"Attempting to retrieve {typeof(TEntity).Name} with id {id} from cache.");

            if (!_cache.TryGetValue(cacheKey, out TEntity entity))
            {
                _logger.LogInformation($"Cache miss for {typeof(TEntity).Name} with id {id}. Fetching from decorated service.");
                entity = await _decoratedService.GetEntityAsync(id);

                _cache.Set(cacheKey, entity, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                });
            }
            else
            {
                _logger.LogInformation($"Cache hit for {typeof(TEntity).Name} with id {id}.");
            }

            return entity;
        }

        private async Task<T> MeasureAndLogTimeAsync<T>(Func<Task<T>> action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            T result = await action();
            stopwatch.Stop();
            TimeSpan elapsed = stopwatch.Elapsed;
            _logger.LogInformation($"Time taken: {elapsed}");
            return result;
        }
    }
}

