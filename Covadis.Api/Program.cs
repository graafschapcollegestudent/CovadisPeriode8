using Covadis.Api.Application.Interfaces;
using Covadis.Api.Application.Services;
using Covadis.Api.Data;
using Covadis.Api.Infrastructure.Repositories;
using Covadis.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// --- Database (InMemory) ---
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("CovadisDb");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.WithOrigins("https://localhost:7138") // jouw blazor poort
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// --- Seed data ---
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    context.Users.Add(new User
    {
        Id = Guid.Parse("66B6F2F6-904D-4ED1-80F3-D571F54B5BBF"),
        Email = "admin@covadis.nl",
        Username = "admin",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
        FullName = "admin oeleh",
        Role = UserRole.Manager,
    });

    var teamId = Guid.NewGuid();

    context.Teams.Add(new Team
    {
        Id = teamId,
        Name = "Team Alpha"
    });

    context.Users.Add(new User
    {
        Id = Guid.NewGuid(),
        Email = "dev@covadis.nl",
        Username = "developer",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("dev123"),
        FullName = "Jan Developer",
        Role = UserRole.Developer,
        TeamId = teamId
    });

    context.SaveChanges();
}



// --- JWT Authenticatie ---
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["Secret"]!;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
    });

// Add services to the container.
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowBlazor");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();