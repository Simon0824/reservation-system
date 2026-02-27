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
        public readonly SignInManager<UserAppModel> _signIn;
        public UserController(UserManager<UserAppModel> userManager, SignInManager<UserAppModel> signIn)
        {
            _userManager = userManager;
            _signIn = signIn;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody]RegisterDto dto)
        {
            UserAppModel user = new()
            {
              Name = dto.Name,
              LastName = dto.LastName,
              Email = dto.Email,
              UserName = dto.Email,
              NormalizedEmail = _userManager.NormalizeEmail(dto.Email)  
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if(!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok("Account created!");
        }

        [HttpPost("login")]
        public async Task <ActionResult> LoginUser([FromBody] LoginDto dto)
        {
            var user  = await _userManager.FindByEmailAsync(dto.Email);
            if(user == null)
            {
                return BadRequest("Wrong email or password!");
            }
            var passwordValid  = await _signIn.CheckPasswordSignInAsync(
                user,
                dto.Password,
                dto.rememberMe
                );
            if(!passwordValid.Succeeded)
            {
                return BadRequest("Wrong email or password!");
            }
            return Ok(new {Message = "You logged properly!"});
        }

        [HttpGet("accounts")]
        public async Task<ActionResult> GetAccounts()
        {
            var accounts = await _userManager.Users.Select(u => new
            {
                u.Id,
                u.Email,
                u.Name,
                u.LastName
            })
            .AsNoTracking()
            .ToListAsync();
            return Ok(accounts);
        }
    }
}