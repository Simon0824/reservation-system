using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace reservation_system.Models;
public class UserAppModel : IdentityUser
{
    [Required]
    [EmailAddress]
    public override string? Email {get; set;}
    [Required]
    public required string Name {get; set;}
    [Required]
    public required string LastName {get; set;}
    public List<ReservationModel> reservations {get; set;} = new();
}
