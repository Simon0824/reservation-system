using reservation_system.Dtos;
using reservation_system.Models;

namespace reservation_system.Services;
public interface IReservationService
{
        Task<ReservationModel> CreateNewReservation(ReservationDto newReservation);
        Task<List<ReservationModel>> GetAllBookedReservations();
}
