
using App.Config.Helpers;
using App.Dependences.Automapper;
using App.Dependences.IoC;
using App.Domain.Interfaces;
using App.Domain.Services;
using App.Infrastructure.Database.Context;
using App.Infrastructure.Interfaces;
using App.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Swagger
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please Enter a Valid Token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        }
    );
    option.AddSecurityRequirement(
        new OpenApiSecurityRequirement
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
                new string[] { }
            }
        }
    );
});

#endregion

#region ConnectionBD

builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStrings"));
});

#endregion

#region SecurityJWT

builder.Services
    .AddHttpContextAccessor()
    .AddAuthorization()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            SaveSigninToken = true,
            ValidIssuer = (builder.Configuration["JwtConfiguration:Issuer"] ?? string.Empty),
            ValidAudience = (builder.Configuration["JwtConfiguration:Audience"] ?? string.Empty),
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["JwtConfiguration:Key"] ?? string.Empty
                )
            )
        };
    });
#endregion

#region InjectionDependences
builder.Services.DependencyInjections();
#endregion

#region InjectionMapper
var configMapper = new MapperConfiguration(options =>
{
    options.AddProfile(new AutomapperProfile());
});

var mapper = configMapper.CreateMapper();
builder.Services.AddSingleton(mapper);

#endregion

#region CORS
const string AllowCors = "AllowCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowCors, x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(AllowCors);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
