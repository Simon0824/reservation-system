using Microsoft.AspNetCore.Identity;
using reservation_system.Dtos;
using reservation_system.Responses;

namespace reservation_system.Services;
public interface IUserService
{
    Task<object> GetAccountsAsync();
    Task<UserServiceResponses> UserLogin(LoginDto dto);
    Task<IdentityResult> UserRegister(RegisterDto dto);
}
