using System.Reflection;
using FluentValidation.AspNetCore;
using MediatR;
using SquashLeagueService.Api.Middleware;
using SquashLeagueService.Application;
using SquashLeagueService.Persistence;
using SquashLeagueService.Infrastructure;
using SquashLeagueService.Infrastructure.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Order is important here since AddIdentity has be called before AddAuthentication
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration); 
builder.Services.AddApplication();

builder.Services.AddFluentValidation(config =>
    config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors((x => x.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()
    ));
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandler>();

app.MapControllers();

app.Run();