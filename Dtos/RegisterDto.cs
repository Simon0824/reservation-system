using System;
using System.ComponentModel.DataAnnotations;

namespace reservation_system.Dtos;

public record class RegisterDto(
    [Required]
    string Name,
    [Required]
    string LastName,
    [Required]
    [EmailAddress]
    string Email,
    [Required]
    [DataType(DataType.Password)]
    string Password
);
