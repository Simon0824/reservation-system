using System;

namespace reservation_system.Models;

public class ReservationModel
{
    public int Id {get; set;}
    public string Name {get; set;}
    public string LastName {get; set;}
    public DateOnly ReservationDate {get; set;}
}
