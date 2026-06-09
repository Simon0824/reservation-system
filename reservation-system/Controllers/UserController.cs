using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using reservation_system.Dtos;
using reservation_system.Services;

namespace reservation_system.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/auth")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody]RegisterDto dto)
        {
            var isUserCreated = await userService.UserRegister(dto);
            if(!isUserCreated.Succeeded)
            {
                return BadRequest(isUserCreated.Errors);
            }
            return Ok("Account created!");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task <ActionResult> LoginUser([FromBody] LoginDto dto)
        {
            var result = await userService.UserLogin(dto);
            return result.Succeeded ? Ok(new { Message = "You logged properly!", Token = result.Token }) : BadRequest(result.Error);
        }

        [HttpGet("accounts")]
        public async Task<ActionResult> GetAccounts()
        {
            var accounts = await userService.GetAccountsAsync();
            return Ok(accounts);
        }
    }
}