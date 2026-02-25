using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace reservation_system.Models;

public class UserAppModel : IdentityUser
{
    [Required]
    [EmailAddress]
    public string Email {get; set;}
    [Required]
    [DataType(DataType.Password)]
    public string Password {get; set;}
    [Required]
    public string Name {get; set;}
    [Required]
    public string LastName {get; set;}
    public List<ReservationModel> reservations {get; set;} = new();
}
