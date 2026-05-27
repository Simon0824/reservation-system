    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using reservation_system.Dtos;
    using reservation_system.Models;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;
    using Microsoft.IdentityModel.JsonWebTokens;

    namespace reservation_system.Controllers
    {
        [Route("api/auth")]
        [ApiController]
        public class UserController(
        IConfiguration configuration,
        UserManager<UserAppModel> userManager,
        SignInManager<UserAppModel> signIn) : ControllerBase
        {
            public readonly UserManager<UserAppModel> _userManager = userManager;
            public readonly SignInManager<UserAppModel> _signIn = signIn;

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
                var token = CreateToken(user);
                return Ok(new {Message = "You logged properly!", Token = token});
            }

            [HttpGet("accounts")]
            public async Task<ActionResult> GetAccounts()
            {
                var accounts = await _userManager.Users.Select(u => new
                {
                    u.Id,
                    u.Email,
                    u.Name,
                    u.LastName,
                    u.reservations
                })
                .AsNoTracking()
                .ToListAsync();
                return Ok(accounts);
            }

            private string CreateToken(UserAppModel user)
            {
                var claims = new Dictionary<string, object>
                {
                   { JwtRegisteredClaimNames.Sub, user.Id },
                   { JwtRegisteredClaimNames.UniqueName, user.UserName },
                   { JwtRegisteredClaimNames.Email, user.Email ?? "" },
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("CreateToken:Token")!));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                Issuer = configuration["CreateToken:Issuer"],
                Audience = configuration["CreateToken:Audience"],
                Claims = claims,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
                };

                return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
            }
        }
    }