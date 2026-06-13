using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using reservation_system.Data;
using reservation_system.Exceptions;
using reservation_system.Models;
using reservation_system.Services;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine($"DEBUG TOKEN: {builder.Configuration["CreatingToken:Token"]}");

builder.Services.AddProblemDetails( configure =>
{
   configure.CustomizeProblemDetails = context =>
   {
       context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
   };
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddDbContext<ReservationContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ConnString")));

    builder.Services.AddIdentityCore<UserAppModel>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
    .AddRoles<IdentityRole>()
    .AddSignInManager()
    .AddEntityFrameworkStores<ReservationContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["CreatingToken:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["CreatingToken:Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["CreatingToken:Token"]!)),
                        ValidateIssuerSigningKey = true
                    };
                }
                );

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Reservation System API", Version = "v1" });


    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Description = "Type only JWT token"
    });

    
c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
{
    [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
});
});

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();


app.MigrateDb();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}


app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
