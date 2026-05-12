using Covadis.Api.Application.Interfaces;
using Covadis.Api.Application.Services;
using Covadis.Api.Data;
using Covadis.Api.Infrastructure.Repositories;
using Covadis.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- Database (InMemory) ---
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("CovadisDb");
});

// --- Seed data ---
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Users.Add(new User
    {        
        Id = Guid.Parse("66B6F2F6-904D-4ED1-80F3-D571F54B5BBF"),
        Username = "admin",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
        FullName = "admin oeleh",
        Role = UserRole.Manager,
    });
    context.SaveChanges();
}

// --- JWT Authenticatie ---
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["Secret"]!;

// Add services to the container.
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();

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
 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();