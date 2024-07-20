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
        public IQueryable<Place> GetAll(PlaceQueryParameters queryParameters) => _context.Places.AsNoTracking()
            .Where(type => type.PlaceName.Contains(queryParameters.Search!.Trim().ToLower()))
                .OrderBy(type => type.PlaceName)
                    .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize).Take(queryParameters.PageSize);

        //GRPC METHOD
        public IQueryable<Place> GetAll(QueryParameters queryParameters) => _context.Places.AsNoTracking()
            .Where(type => type.PlaceName.Contains(queryParameters.Search!.Trim().ToLower()))
                .OrderBy(type => type.PlaceName)
                    .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize).Take(queryParameters.PageSize);

        public async Task<Place?> GetById(Guid id) => await _context.Places.FindAsync(id);
    }
}
