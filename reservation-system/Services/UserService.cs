using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using reservation_system.Dtos;
using reservation_system.Models;
using reservation_system.Responses;

namespace reservation_system.Services
{
    public class UserService(UserManager<UserAppModel> userMan, SignInManager<UserAppModel> signMan, ITokenService tokenService) : IUserService
    {
        public async Task<IdentityResult> UserRegister(RegisterDto dto)
        {
            UserAppModel user = new()
            {
                Name = dto.Name,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.Email,
                NormalizedEmail = userMan.NormalizeEmail(dto.Email)
            }; 
            return await userMan.CreateAsync(user, dto.Password);
        }

        public async Task<UserServiceResponses> UserLogin(LoginDto dto)
        {
            var user = await userMan.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return new UserServiceResponses { Succeeded = false, Error = "Wrong email or password!" };
            }
            var passwordValid = await signMan.CheckPasswordSignInAsync(
            user,
            dto.Password,
            dto.rememberMe
            );
            if (!passwordValid.Succeeded)
            {
                return new UserServiceResponses { Succeeded = false, Error = "Wrong email or password!" };
            }
            var token = tokenService.CreateToken(user);
            return new UserServiceResponses { Succeeded = true, Token = token };
        }

        public async Task<object> GetAccountsAsync()
        {
            return await userMan.Users
            .Select(u => new
            {
                u.Id,
                u.Email,
                u.Name,
                u.LastName,
                u.reservations
            })
            .AsNoTracking()
            .ToListAsync();
        }
    }
}