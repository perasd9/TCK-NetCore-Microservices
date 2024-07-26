using Ardalis.ApiEndpoints;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Reservations.API.Core;
using Reservations.API.DTOs;
using Users.API.Application;

namespace Reservations.API.Endpoints
{
    public class Add : EndpointBaseAsync
        .WithRequest<CreateReservationDTO>
        .WithActionResult
    {
        private readonly ReservationService _reservationService;
        private readonly IMapper _mapper;

        public Add(ReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        public override Task<ActionResult> HandleAsync(CreateReservationDTO request, CancellationToken cancellationToken = default)
        {
            var reservation = _mapper.Map<Reservation>(request);

            _reservationService.
        }
    }
}
