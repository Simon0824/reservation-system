using Xunit;
using reservation_system.Models;
using reservation_system.Services; 
using reservation_system.Dtos;
using reservation_system.Data;
using Microsoft.EntityFrameworkCore;
namespace Tests.UnitTest;

public class ReservationTest
{
    private static readonly ReservationDto dto = new ReservationDto(new DateOnly(2026, 12, 24));
    private ReservationContext CreateContext(string Db)
    {
        var options = new DbContextOptionsBuilder<ReservationContext>()
            .UseInMemoryDatabase(Db)
            .Options;
            return new ReservationContext(options);
    }

    [Fact]
    public async Task ReservationService_Should_ReturnNull_WhenDateIsTaken()
    {
        //Arrange
        var context = CreateContext("taken_date");
        var takenDate = new DateOnly(2026, 12, 24);
        var _reservationService = new ReservationService(context);
        context.Reservation.Add(new ReservationModel { ReservationDate = takenDate});
        await context.SaveChangesAsync();
        //Assert & Act
        await Assert.ThrowsAsync<ApplicationException>(() => _reservationService.CreateNewReservation(dto));
    }

    [Fact]
    public async Task ReservationService_Should_ReturnDto_WhenDateIsFree()
    {
        //Arrange
        var context = CreateContext("free_date");
        var _reservationService = new ReservationService(context);
        //Act
        var result = await _reservationService.CreateNewReservation(dto);
        //Assert
        Assert.NotNull(result);
        Assert.Equal(dto.ReservationDate, result.ReservationDate);
    }
}