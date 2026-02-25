using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using reservation_system.Models;

namespace reservation_system.Data;

public class ReservationContext: IdentityDbContext<IdentityUser>
{
    public ReservationContext(DbContextOptions<ReservationContext> opt) : base(opt){} 
    public DbSet<ReservationModel> Reservation => Set<ReservationModel>();
}
