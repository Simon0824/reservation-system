using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reservation_system.Data;
using reservation_system.Dtos;
using reservation_system.Models;
namespace reservation_system.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationContext _context;
        private readonly UserManager<UserAppModel> _userManager;

        public ReservationController(ReservationContext context, UserManager<UserAppModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReservationDto>> Create(ReservationDto newReservation)
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return BadRequest(new {Message = "You're not logged"});
            }
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
            user.reservations.Add(reservation);
            await _userManager.UpdateAsync(user);
            return CreatedAtAction(nameof(Get), reservation);
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
