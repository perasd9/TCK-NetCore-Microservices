﻿using Microsoft.EntityFrameworkCore;
using TypesOfSportingEvents.API.Core;
using TypesOfSportingEvents.API.Core.Interfaces.UnitOfWork;
using TypesOfSportingEvents.API.Core.Pagination;
using TypesOfSportingEvents.API.Endpoints.QueryParameters;

namespace TypesOfSportingEvents.API.Application
{
    public class TypeOfSportingEventService
    {
        private IUnitOfWork _unitOfWork;

        public TypeOfSportingEventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationList<TypeOfSportingEvent>> GetAll(TypeOfSportingEventQueryParameters queryParameters)
        {
            var items = await _unitOfWork.TypeOfSportingEventRepository.GetAll(queryParameters).ToListAsync();


            return new PaginationList<TypeOfSportingEvent>(items, items.Count, queryParameters.PageNumber, queryParameters.PageSize);
        }
        public async Task<TypeOfSportingEvent?> GetById(Guid id)
        {
            return await _unitOfWork.TypeOfSportingEventRepository.GetById(id);
        }
    }
}
