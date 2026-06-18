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
using Task = Covadis.Api.Models.Task;
using TaskItem = Covadis.Api.Models.Task;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("CovadisDb");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.WithOrigins("*")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["Secret"] ?? throw new InvalidOperationException("JwtSettings:Secret missing");

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

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
        Description = "Enter JWT with 'Bearer ' prefix",
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

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var adminUser = new User
    {
        Id = Guid.Parse("262687C1-CC09-4DEB-A510-AE4ABE416B3F"),
        Username = "admin",
        Email = "admin@gmail.com",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
        FullName = "admin",
        Role = UserRole.Manager,
    };

    var normalUser = new User
    {
        Id = Guid.Parse("084AB2DC-C977-440A-8785-1C73EBF41908"),
        Username = "user",
        Email = "user@gmail.com",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("user"),
        FullName = "user",
        Role = UserRole.Developer,
    };


    var teamId1 = Guid.NewGuid();

    var team = new Team
    {
        Id = teamId1,
        Name = "Team1",
    };

    var teamId2 = Guid.NewGuid();
    var team2 = new Team
    {
        Id = teamId2,
        Name = "Team2",
    };

    var developer2 = new User
    {
        Id = Guid.NewGuid(),
        Username = "developer2",
        Email = "dev2@gmail.com",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("dev2"),
        FullName = "Jan Jansen",
        Role = UserRole.Developer,
        TeamId = teamId2
    };

    var teamId3 = Guid.NewGuid();
    var team3 = new Team
    {
        Id = teamId3,
        Name = "Team3",
    };

    var developer3 = new User
    {
        Id = Guid.NewGuid(),
        Username = "developer3",
        Email = "dev3@gmail.com",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("dev3"),
        FullName = "Piet Pietersen",
        Role = UserRole.Developer,
        TeamId = teamId3
    };

    context.Users.Add(developer2);
    context.Teams.Add(team2);

    context.Users.Add(developer3);
    context.Teams.Add(team3);

    var task1 = new Covadis.Api.Models.Task
    {
        Id = Guid.NewGuid(),
        Title = "Taak 1",
        Description = "Dit is een testtaak",
        TeamId = teamId1,
        DueDate = DateTime.Now.AddDays(7),
        EstimatedDuration = 8
    };
    var task2 = new Covadis.Api.Models.Task
    {
        Id = Guid.NewGuid(),
        Title = "Taak 2",
        Description = "Korte taak",
        TeamId = teamId2,
        DueDate = DateTime.Now.AddDays(3),
        EstimatedDuration = 2
    };
    for (int i = 3; i <= 12; i++)
    {
        context.Tasks.Add(new Covadis.Api.Models.Task
        {
            Id = Guid.NewGuid(),
            Title = $"Taak {i}",
            Description = $"Omschrijving taak {i}",
            TeamId = teamId3,
            DueDate = DateTime.Now.AddDays(i),
            EstimatedDuration = i
        });
    }
    
    context.Tasks.Add(task2);

    context.Tasks.Add(task1);

    adminUser.TeamId = teamId1;
    normalUser.TeamId = teamId1;

    context.Teams.Add(team);
    context.Users.AddRange(adminUser, normalUser);
    context.SaveChanges();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowBlazor");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();