using Microsoft.AspNetCore.Identity;
using reservation_system.Dtos;
using reservation_system.Models;
using reservation_system.Services;
using NSubstitute;
using Xunit;
using Microsoft.AspNetCore.Http;
public class UserServiceTest
{
    private readonly UserManager<UserAppModel> _userMan;
    private readonly UserService _userService;
    private readonly SignInManager<UserAppModel> _signMan;
    private readonly ITokenService _tokenService;
    private readonly RegisterDto registerDto = new RegisterDto(Email: "example@email.com", Password: "P4$$w0rD", Name: "Name", LastName: "LastName");

    public UserServiceTest()
    {
        var userStore = Substitute.For<IUserStore<UserAppModel>>();
        _userMan = Substitute.For<UserManager<UserAppModel>>(
            userStore, null, null, null, null, null, null, null, null);

        var contextAccessor = Substitute.For<IHttpContextAccessor>();
        var claimsFactory = Substitute.For<IUserClaimsPrincipalFactory<UserAppModel>>();

        _signMan = Substitute.For<SignInManager<UserAppModel>>(
            _userMan, contextAccessor, claimsFactory, null, null, null, null);

        _tokenService = Substitute.For<ITokenService>();

        _userService = new UserService(_userMan, _signMan, _tokenService);
    }
    [Fact]
    public async Task UserRegister_Should_ReturnErrors_WhenUserExist()
    {
        //Arrange
        UserAppModel existingUser = new()
        {
           Email = registerDto.Email,
           PasswordHash = registerDto.Password, 
           Name = registerDto.Name, 
           LastName = registerDto.LastName
        };
        _userMan.CreateAsync(Arg.Any<UserAppModel>(), Arg.Any<string>())
             .Returns(IdentityResult.Failed(new IdentityError { Description = "User already exists" }));

        //Act
        var registerResult = await _userService.UserRegister(registerDto);
        
        //Assert
        Assert.False(registerResult.Succeeded);
        Assert.NotEmpty(registerResult.Errors);
    }
}