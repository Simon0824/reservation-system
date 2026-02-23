using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace reservation_system.Dtos;

public record class ReservationDto(
    [Required]string Name,
    [Required]string LastName,
    [Required]DateOnly ReservationDate
);
