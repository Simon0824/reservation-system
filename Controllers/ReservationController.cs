using System.Xml.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<ReservationDto>> Create(ReservationDto newReservation)
        {
            ReservationModel reservation = new()
            {
               ReservationDate = newReservation.ReservationDate,
            };
            bool DateTaken = await _context.Reservation.AnyAsync(r => r.ReservationDate == newReservation.ReservationDate);
            if(DateTaken)
            {
                return BadRequest(new {Message = "That date is already taken!"});
            }
            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), newReservation);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get()
        {
            var reservation = await _context.Reservation.AsNoTracking().ToListAsync();
            return Ok(reservation);
        }
    }
}
