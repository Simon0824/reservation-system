using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using reservation_system.Models;

namespace reservation_system.Data;

public class ReservationContext(DbContextOptions<ReservationContext> opt) : DbContext(opt)
{
    public DbSet<ReservationModel> Reservation => Set<ReservationModel>();
}
