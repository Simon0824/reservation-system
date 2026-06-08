using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reservation_system.Data;
using reservation_system.Dtos;
using reservation_system.Models;
using reservation_system.Services;
namespace reservation_system.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationContext _context;
        private readonly UserManager<UserAppModel> _userManager;
        private readonly IReservationService _reservService;

        public ReservationController(ReservationContext context, UserManager<UserAppModel> userManager, IReservationService reservService)
        {
            _context = context;
            _userManager = userManager;
            _reservService = reservService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReservationModel>> Create([FromBody]ReservationDto newReservation)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                {
                    return Unauthorized(new { Message = "You are not authorized" });
                }
            var result = await _reservService.CreateNewReservation(newReservation);
            if(result == null)
                {
                    return BadRequest(new { Message = "Date is already taken!" });
                }
            user.reservations.Add(result);
            await _userManager.UpdateAsync(user);
            return CreatedAtAction(nameof(Get), result);
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
