using Mapster;
using SportingEvents.API.Core;
using SportingEvents.API.DTOs;

namespace SportingEvents.API.Endpoints.Mapster
{
    public static class MapsterConfig
    {
        public static void Configure()
        {
            //SportingEvent to GetSportingEvent
            TypeAdapterConfig<SportingEvent, GetSportingEventDTO>.NewConfig()
                .Map(dest => dest.SportingEventId, src => src.SportingEventId)
                .Map(dest => dest.SportingEventName, src => src.SportingEventName)
                .Map(dest => dest.SportingEventDescription, src => src.SportingEventDescription)
                .Map(dest => dest.SportingEventTicketPrice, src => src.SportingEventTicketPrice)
                .Map(dest => dest.DateOfSportingEvent, src => src.DateOfSportingEvent)
                .Map(dest => dest.TypeOfSportingEvent, src => src.TypeOfSportingEvent)
                .Map(dest => dest.User, src => src.User)
                .Map(dest => dest.AvailableTickets, src => src.AvailableTickets);

            //CreateSportingEvent to SportingEvent
            TypeAdapterConfig<CreateSportingEventDTO, SportingEvent>.NewConfig()
                .Map(dest => dest.SportingEventName, src => src.SportingEventName)
                .Map(dest => dest.SportingEventDescription, src => src.SportingEventDescription)
                .Map(dest => dest.SportingEventTicketPrice, src => src.SportingEventTicketPrice)
                .Map(dest => dest.DateOfSportingEvent, src => src.DateOfSportingEvent)
                .Map(dest => dest.TypeOfSportingEventId, src => src.TypeOfSportingEventId)
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.AvailableTickets, src => src.AvailableTickets);

            //UpdateSportingEvent to Sporting Event
            TypeAdapterConfig<UpdateSportingEventDTO, SportingEvent>.NewConfig()
                .Map(dest => dest.SportingEventId, src => src.SportingEventId)
                .Map(dest => dest.SportingEventName, src => src.SportingEventName)
                .Map(dest => dest.SportingEventDescription, src => src.SportingEventDescription)
                .Map(dest => dest.SportingEventTicketPrice, src => src.SportingEventTicketPrice)
                .Map(dest => dest.DateOfSportingEvent, src => src.DateOfSportingEvent)
                .Map(dest => dest.TypeOfSportingEventId, src => src.TypeOfSportingEventId)
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.AvailableTickets, src => src.AvailableTickets);

            //DeleteSportingEvent to SportingEvent
            TypeAdapterConfig<DeleteSportingEventDTO, SportingEvent>.NewConfig()
                .Map(dest => dest.SportingEventId, src => src.SportingEventId)
                .Map(dest => dest.TypeOfSportingEventId, src => src.TypeOfSportingEventId)
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.AvailableTickets, src => src.AvailableTickets);
        }
    }
}
