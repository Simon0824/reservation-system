using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using reservation_system.Data;
using reservation_system.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ReservationContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ConnString")));

builder.Services.AddIdentity<UserAppModel, IdentityRole>()
                .AddEntityFrameworkStores<ReservationContext>()
                .AddApiEndpoints()
                .AddDefaultTokenProviders()
                .AddTokenProvider<DataProtectorTokenProvider<UserAppModel>>(IdentityConstants.BearerScheme);
builder.Services.AddAuthentication(IdentityConstants.BearerScheme)
                .AddBearerToken(IdentityConstants.BearerScheme);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MigrateDb();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();

app.Run();
