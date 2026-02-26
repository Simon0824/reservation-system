using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reservation_system.Dtos;
using reservation_system.Models;

namespace reservation_system.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly UserManager<UserAppModel> _userManager;
        public UserController(UserManager<UserAppModel> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody]RegisterDto dto)
        {
            UserAppModel user = new()
            {
              Name = dto.Name,
              LastName = dto.LastName,
              Email = dto.Email,
              Password = dto.Password,
              UserName = dto.Email  
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if(!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Created();
        }

        [HttpGet("accounts")]
        public async Task<ActionResult> GetAccounts()
        {
            var accounts = await _userManager.Users.Select(u => new
            {
                u.Id,
                u.Email
            })
            .AsNoTracking()
            .ToListAsync();
            return Ok(accounts);
        }
    }
}
