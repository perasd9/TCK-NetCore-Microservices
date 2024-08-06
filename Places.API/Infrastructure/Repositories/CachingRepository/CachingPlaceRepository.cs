using Microsoft.Extensions.Caching.Memory;
using Places.API.Core;
using Places.API.Core.Interfaces;
using Places.API.Core.Protos;
using Places.API.Endpoints.QueryParameters;

namespace Places.API.Infrastructure.Repositories.CachingRepository
{
    public class CachingPlaceRepository : IPlaceRepository
    {
        private readonly PlaceRepository _placeRepository;
        private readonly IMemoryCache _memoryCache;

        public CachingPlaceRepository(PlaceRepository placeRepository, IMemoryCache memoryCache)
        {
            _placeRepository = placeRepository;
            _memoryCache = memoryCache;
        }

        public Task<List<Place>> GetAll(PlaceQueryParameters queryParameters)
        {
            return _memoryCache.GetOrCreate("places", (cacheEntry) =>
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                return _placeRepository.GetAll(queryParameters);
            })!;
        }

        public Task<List<Place>> GetAll(QueryParameters queryParameters)
        {
            return _memoryCache.GetOrCreate("places", (cacheEntry) =>
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                return _placeRepository.GetAll(queryParameters);
            })!;
        }

        public Task<Place?> GetById(Guid id)
        {
            return _memoryCache.GetOrCreate($"places-{id}", (cacheEntry) =>
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                return _placeRepository.GetById(id);
            })!;
        }
    }
}
