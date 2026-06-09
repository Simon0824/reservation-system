using reservation_system.Models;

namespace reservation_system.Services;
public interface ITokenService
{
    string CreateToken(UserAppModel user);
}