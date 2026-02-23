using System;

namespace reservation_system.Dtos;

public record class Reservation(
    string Name,
    string LastName,
    DateOnly ReservationDate
);
