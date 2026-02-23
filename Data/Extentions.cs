using System;
using System.Xml.Schema;
using Microsoft.EntityFrameworkCore;
namespace reservation_system.Data;

public static class Extentions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ReservationContext>();
        context.Database.Migrate();
    }
}
