using Ardalis.ApiEndpoints;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Reservations.API.Application;
using Reservations.API.Core;
using Reservations.API.Core.Abstractions;
using Reservations.API.DTOs;

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

        [HttpPost("api/v1/reservations")]
        public async override Task<ActionResult> HandleAsync([FromBody] CreateReservationDTO request, CancellationToken cancellationToken = default)
        {
            var reservation = _mapper.Map<Reservation>(request);

            var result = await _reservationService.Add(reservation);

            return result.IsSuccess ? Ok("Reservation created!") : ApiResults.Problem(result);
        }
    }
}
