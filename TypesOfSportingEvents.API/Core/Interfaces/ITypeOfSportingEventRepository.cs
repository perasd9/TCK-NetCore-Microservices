using System.Linq.Expressions;
using TypesOfSportingEvents.API.Core.Protos;
using TypesOfSportingEvents.API.Endpoints.QueryParameters;

namespace TypesOfSportingEvents.API.Core.Interfaces
{
    public interface ITypeOfSportingEventRepository
    {
        //REST METHOD
        public Task<List<TypeOfSportingEvent>> GetAll(TypeOfSportingEventQueryParameters queryParameters);
        //GRPC METHOD
        public Task<List<TypeOfSportingEvent>> GetAll(QueryParameters queryParameters);
        //COMMON METHOD, DOESN'T NEED BOTH
        public Task<TypeOfSportingEvent?> GetById(Guid id);
    }
}
