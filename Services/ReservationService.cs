using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using reservation_system.Data;
using reservation_system.Dtos;
using reservation_system.Models;
namespace reservation_system.Services
{

    public class ReservationService : IReservationService
    {
        private readonly ReservationContext _context;

        public ReservationService(ReservationContext context)
        {
            _context = context;
        }

        public async Task<ReservationModel> CreateNewReservation(ReservationDto newReservation)
        {
            bool DateTaken = await _context.Reservation.AnyAsync(r => r.ReservationDate == newReservation.ReservationDate);
            if (DateTaken)
            {
                return null;
            }
            ReservationModel reservation = new()
            {
                ReservationDate = newReservation.ReservationDate,
            };
            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }
    }

    public interface IReservationService
    {
        Task<ReservationModel> CreateNewReservation(ReservationDto newReserv);
    }
}