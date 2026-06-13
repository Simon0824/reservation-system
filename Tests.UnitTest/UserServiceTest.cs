using Microsoft.AspNetCore.Identity;
using reservation_system.Dtos;
using reservation_system.Models;
using reservation_system.Services;
using NSubstitute;
using Xunit;
using Microsoft.AspNetCore.Http;
using reservation_system.Responses;
public class UserServiceTest
{
    private readonly UserManager<UserAppModel> _userMan;
    private readonly UserService _userService;
    private readonly SignInManager<UserAppModel> _signMan;
    private readonly ITokenService _tokenService;
    private readonly RegisterDto registerDto = new RegisterDto(Email: "example@email.com", Password: "P4$$w0rD", Name: "Name", LastName: "LastName");
    private readonly LoginDto loginDto = new LoginDto(Email: "example@email.com", Password: "P4$$w0rD", rememberMe: true);
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
           Name = registerDto.Name, 
           LastName = registerDto.LastName
        };
        _userMan.CreateAsync(Arg.Is<UserAppModel>(u =>
              u.Email == existingUser.Email &&
              u.Name == existingUser.Name &&
              u.LastName == existingUser.LastName), Arg.Is<string>(p => p == registerDto.Password))
             .Returns(IdentityResult.Failed(new IdentityError { Description = "User already exists" }));

        //Act
        var registerResult = await _userService.UserRegister(registerDto);
        
        //Assert
        Assert.False(registerResult.Succeeded);
        Assert.NotEmpty(registerResult.Errors);
    }

    [Fact]
    public async Task UserRegister_Should_ReturnSucced_WhenUserIsRegistered()
    {
        //Arrange
        _userMan.CreateAsync(Arg.Is<UserAppModel>(u =>
              u.Email == registerDto.Email &&
              u.Name == registerDto.Name &&
              u.LastName == registerDto.LastName), Arg.Is<string>(p => p == registerDto.Password))
             .Returns(IdentityResult.Success);
        //Act
        var registerResult = await _userService.UserRegister(registerDto);
        //Assert
        Assert.True(registerResult.Succeeded);
    }

    [Fact]
    public async Task UserLogin_Should_ReturnError_WhenEmailIsWrong()
    {
        //Arrange
        _userMan.FindByEmailAsync(Arg.Is<string>(l => l == loginDto.Email)).Returns((UserAppModel?)null);
        //Act
        var loginResult = await _userService.UserLogin(loginDto);
        //Assert
        Assert.False(loginResult.Succeeded);
        Assert.NotNull(loginResult.Error);
    }

    [Fact]
    public async Task UserLogin_Should_ReturnError_WhenPasswordIsWrong()
    {
        //Arrange
        _signMan.CheckPasswordSignInAsync(Arg.Is<UserAppModel>(l =>
             l.Email == loginDto.Email &&
             l.PasswordHash == loginDto.Password)
             , Arg.Is<string>(l => l == loginDto.Password)
             , Arg.Is<bool>(l => l == true))
             .Returns(SignInResult.Failed);
        //Act
        var loginResult = await _userService.UserLogin(loginDto);
        //Assert
        Assert.False(loginResult.Succeeded);
        Assert.NotNull(loginResult.Error);
    }

    [Fact]
    public async Task UserLogin_Should_ReturnSuceeded_WhenLoginIsValid()
    {
        //Arrange
        _userMan.FindByEmailAsync(Arg.Is<string>(l => l == loginDto.Email)).Returns(new UserAppModel 
        {
            Email = loginDto.Email,
            PasswordHash = loginDto.Password,
            Name = registerDto.Name,
            LastName = registerDto.LastName
        });

        _signMan.CheckPasswordSignInAsync(Arg.Is<UserAppModel>(l =>
             l.Email == loginDto.Email &&
             l.PasswordHash == loginDto.Password &&
             l.Name == registerDto.Name &&
             l.LastName == registerDto.LastName)
             , Arg.Is<string>(l => l == loginDto.Password)
             , Arg.Is<bool>(l => l == loginDto.rememberMe))
             .Returns(SignInResult.Success);
        
        var expectedToken = "mocked-jwt-token-string";

        _tokenService.CreateToken(Arg.Is<UserAppModel>(u => u.Email == loginDto.Email))
             .Returns(expectedToken);
        //Act
        var loginResult = await _userService.UserLogin(loginDto);
        //Assert
        Assert.True(loginResult.Succeeded);
        Assert.Null(loginResult.Error);
        Assert.NotNull(loginResult.Token);
    }
}