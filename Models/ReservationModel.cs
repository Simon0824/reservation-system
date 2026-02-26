using System;
using System.ComponentModel.DataAnnotations;

namespace reservation_system.Models;

public class ReservationModel
{
    public int Id {get; set;}
    [Required]
    public DateOnly ReservationDate {get; set;}
}
