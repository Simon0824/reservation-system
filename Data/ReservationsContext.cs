using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using reservation_system.Models;

namespace reservation_system.Data;
public class ReservationContext: IdentityDbContext<UserAppModel>
{
    public ReservationContext(DbContextOptions<ReservationContext> opt) : base(opt){} 
    public DbSet<ReservationModel> Reservation => Set<ReservationModel>();
}
