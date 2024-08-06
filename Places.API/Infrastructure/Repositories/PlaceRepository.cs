using Microsoft.EntityFrameworkCore;
using Places.API.Core;
using Places.API.Core.Interfaces;
using Places.API.Core.Protos;
using Places.API.Endpoints.QueryParameters;

namespace Places.API.Infrastructure.Repositories
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly PlacesContext _context;

        public PlaceRepository(PlacesContext context)
        {
            _context = context;
        }

        //REST METHOD
        public async Task<List<Place>> GetAll(PlaceQueryParameters queryParameters) => await _context.Places.AsNoTracking()
            .Where(type => type.PlaceName.Contains(queryParameters.Search!.Trim().ToLower()))
                .OrderBy(type => type.PlaceName)
                    .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize).Take(queryParameters.PageSize).ToListAsync();

        //GRPC METHOD
        public async Task<List<Place>> GetAll(QueryParameters queryParameters) => await _context.Places.AsNoTracking()
            .Where(type => type.PlaceName.Contains(queryParameters.Search!.Trim().ToLower()))
                .OrderBy(type => type.PlaceName)
                    .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize).Take(queryParameters.PageSize).ToListAsync();

        public async Task<Place?> GetById(Guid id) => await _context.Places.FindAsync(id);
    }
}
