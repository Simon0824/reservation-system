using System.ComponentModel.DataAnnotations;

namespace reservation_system.Dtos;
public record class ReservationDto(
    [Required]
    DateOnly ReservationDate
);
