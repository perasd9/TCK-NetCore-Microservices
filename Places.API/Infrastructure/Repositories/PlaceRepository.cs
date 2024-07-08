using Places.API.Core;
using Places.API.Core.Interfaces;

namespace Places.API.Infrastructure.Repositories
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly PlacesContext _context;

        public PlaceRepository(PlacesContext context)
        {
            _context = context;
        }

        public IQueryable<Place> GetAll()
        {
            return _context.Places;
        }
    }
}
