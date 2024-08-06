using Microsoft.Extensions.Caching.Memory;
using TypesOfSportingEvents.API.Core;
using TypesOfSportingEvents.API.Core.Interfaces;
using TypesOfSportingEvents.API.Core.Protos;
using TypesOfSportingEvents.API.Endpoints.QueryParameters;

namespace TypesOfSportingEvents.API.Infrastructure.CachingRepository
{
    public class CachingTypeOfSportingEventRepository : ITypeOfSportingEventRepository
    {
        private readonly ITypeOfSportingEventRepository _typeOfSportingEventRepository;
        private readonly IMemoryCache _memoryCache;

        public CachingTypeOfSportingEventRepository(IMemoryCache memoryCache, ITypeOfSportingEventRepository typeOfSportingEventRepository)
        {
            _memoryCache = memoryCache;
            _typeOfSportingEventRepository = typeOfSportingEventRepository;
        }

        public Task<List<TypeOfSportingEvent>> GetAll(TypeOfSportingEventQueryParameters queryParameters)
        {
            return _memoryCache.GetOrCreate("types", (cacheEntry) =>
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                return _typeOfSportingEventRepository.GetAll(queryParameters);
            })!;
        }

        public Task<List<TypeOfSportingEvent>> GetAll(QueryParameters queryParameters)
        {
            return _memoryCache.GetOrCreate("types", (cacheEntry) =>
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                return _typeOfSportingEventRepository.GetAll(queryParameters);
            })!;
        }

        public Task<TypeOfSportingEvent?> GetById(Guid id)
        {
            return _memoryCache.GetOrCreate($"types-{id}", (cacheEntry) =>
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                return _typeOfSportingEventRepository.GetById(id);
            })!;
        }
    }
}
