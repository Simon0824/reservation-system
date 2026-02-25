using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using reservation_system.Data;
using reservation_system.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<ReservationContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ConnString")));

builder.Services.AddIdentityCore<UserAppModel>()
                .AddRoles<IdentityUser>()
                .AddEntityFrameworkStores<ReservationContext>()
                .AddApiEndpoints()
                .AddDefaultTokenProviders();

builder.Services.AddAuthentication(IdentityConstants.BearerScheme).AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddAuthorization();

var app = builder.Build();

app.MigrateDb();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();

app.Run();
