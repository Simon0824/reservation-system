using System;
using System.ComponentModel.DataAnnotations;

namespace reservation_system.Dtos;
public record class RegisterDto(
    [Required]
    [EmailAddress]
    string Email,
    [Required]
    [DataType(DataType.Password)]
    string Password,
    [Required]
    string Name,
    [Required]
    string LastName
);
