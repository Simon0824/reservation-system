using System;
using Microsoft.AspNetCore.Identity;

namespace reservation_system.Models;

public class UserAppModel : IdentityUser
{
    public string Email {get; set;}
    public string Password {get; set;}
    public string Name {get; set;}
    public string LastName {get; set;}
}
