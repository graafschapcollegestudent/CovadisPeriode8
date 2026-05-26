using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Covadis.Api.Application.Interfaces;
using Covadis.Api.Application.Services;
using Covadis.Api.Infrastructure.Repositories;
using Covadis.Api.Data;
using Covadis.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

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
    context.SaveChanges();
}

// --- JWT Authenticatie ---
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["Secret"] ?? throw new InvalidOperationException("JwtSettings:Secret missing");

// DI registrations
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Covadis API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT with 'Bearer ' prefix, e.g. \"Bearer {token}\"",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Ensure DB schema exists and seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var adminUser = new User
    {
        Username = "admin",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
        FullName = "admin",
        Role = UserRole.Manager,
    };

    var normalUser = new User
    {
        Id = Guid.Parse("084AB2DC-C977-440A-8785-1C73EBF41908"),
        Username = "user",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("user"),
        FullName = "user",
        Role = UserRole.Developer,
    };

    var team = new Team
    {
        Name = "Team1",
        Users = [ adminUser, normalUser ],
    };

    context.Teams.Add(team);
    context.SaveChanges();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();