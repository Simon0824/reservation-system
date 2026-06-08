using System.ComponentModel.DataAnnotations;

namespace reservation_system.Dtos;
public record class LoginDto(
    [Required]
    [EmailAddress]
    string Email,
    [Required]
    [DataType(DataType.Password)]
    string Password,
    bool rememberMe
);
