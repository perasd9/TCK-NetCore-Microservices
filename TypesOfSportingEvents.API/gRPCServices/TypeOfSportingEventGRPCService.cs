﻿using Grpc.Core;
using TypesOfSportingEvents.API.Application;
using TypesOfSportingEvents.API.Core.Protos;

namespace TypesOfSportingEvents.API.gRPCServices
{
    public class TypeOfSportingEventGRPCService : gRPCTypeOfSportingEventService.gRPCTypeOfSportingEventServiceBase
    {
        private readonly TypeOfSportingEventService _typeOfSportingEventService;

        public TypeOfSportingEventGRPCService(TypeOfSportingEventService typeOfSportingEventService)
        {
            this._typeOfSportingEventService = typeOfSportingEventService;
        }

        public async override Task<PaginationList> GetAll(QueryParameters request, ServerCallContext context)
        {
            request.PageNumber = 1;
            request.PageSize = int.MaxValue;
            var types = (await _typeOfSportingEventService.GetAll(request)).Value;

            var pagination = new PaginationList
            {
                HasNext = types.HasNext,
                HasPrevious = types.HasPrevious,
                PageIndex = types.PageIndex,
                PageSize = types.PageSize,
                TotalPages = types.TotalPages,
                Types_ = {types.Items.Select(type => new TypeOfSportingEventGrpc
                {
                    TypeOfSportingEventId = type.TypeOfSportingEventId.ToString(),
                    TypeOfSportingEventName = type.TypeOfSportingEventName
                }).ToList()}
            };

            return pagination;
        }

        public async override Task<TypeOfSportingEventGrpc> GetById(UUID request, ServerCallContext context)
        {
            var type = (await _typeOfSportingEventService.GetById(request)).Value;

            return new TypeOfSportingEventGrpc { TypeOfSportingEventId = type?.TypeOfSportingEventId.ToString(), TypeOfSportingEventName = type?.TypeOfSportingEventName };
        }
    }
}
