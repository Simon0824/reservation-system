using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using reservation_system.Dtos;
using reservation_system.Models;
using reservation_system.Responses;

namespace reservation_system.Services
{
    public interface IUserService
    {
        Task<object> GetAccountsAsync();
        Task<UserServiceResponses> UserLogin(LoginDto dto);
        Task<IdentityResult> UserRegister(RegisterDto dto);
    }

    public class UserService(UserManager<UserAppModel> userMan, SignInManager<UserAppModel> signMan, ITokenService tokenService) : IUserService
    {
        private readonly UserManager<UserAppModel> _userMan = userMan;
        private readonly SignInManager<UserAppModel> _signMan = signMan;
        private readonly ITokenService _tokenService = tokenService;
        public async Task<IdentityResult> UserRegister(RegisterDto dto)
        {
            UserAppModel user = new()
            {
                Name = dto.Name,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.Email,
                NormalizedEmail = _userMan.NormalizeEmail(dto.Email)
            };
            var result = await _userMan.CreateAsync(user, dto.Password);
            return result;
        }

        public async Task<UserServiceResponses> UserLogin(LoginDto dto)
        {
            var user = await _userMan.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return new UserServiceResponses { Succeeded = false, Error = "Wrong email or password!" };
            }
            var passwordValid = await _signMan.CheckPasswordSignInAsync(
            user,
            dto.Password,
            dto.rememberMe
            );
            if (!passwordValid.Succeeded)
            {
                return new UserServiceResponses { Succeeded = false, Error = "Wrong email or password!" };
            }
            var token = _tokenService.CreateToken(user);
            return new UserServiceResponses { Succeeded = true, Token = token };
        }

        public async Task<object> GetAccountsAsync()
        {
            return await _userMan.Users
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