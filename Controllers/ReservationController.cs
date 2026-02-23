using System.Xml.Schema;
using Microsoft.AspNetCore.Mvc;
using reservation_system.Data;
using reservation_system.Dtos;
using reservation_system.Models;
namespace reservation_system.Controllers
{
    [Route("api/reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationContext _context;

        public ReservationController(ReservationContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ReservationDto> Create(ReservationDto newReservation)
        {
            ReservationModel reservation = new()
            {
               Name = newReservation.Name,
               LastName = newReservation.LastName,
               ReservationDate = newReservation.ReservationDate,
            };
            foreach(var reser in _context.Reservation)
            {
                if(newReservation.ReservationDate == reser.ReservationDate)
                {
                    return BadRequest();
                }
            }
            _context.Reservation.Add(reservation);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), newReservation);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get()
        {
            var reservation = _context.Reservation.ToList();
            return Ok(reservation);
        }
    }
}
