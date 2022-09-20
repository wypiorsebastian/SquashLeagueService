using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SquashLeagueService.Domain.Entities;
using SquashLeagueService.Persistence.Repositories.UnitOfWork;

namespace SquashLeagueService.Persistence;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SQLiteDb");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        
        services.Configure<IdentityOptions>(options =>
        {
            
            options.Password.RequiredLength = 6;
            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
        });
        
        return services;
    }
}