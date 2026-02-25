using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace reservation_system.Dtos;

public record class ReservationDto(
    [Required]DateOnly ReservationDate
);
