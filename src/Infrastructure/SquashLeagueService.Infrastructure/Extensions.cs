using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SquashLeagueService.Application.Contracts.Identity;
using SquashLeagueService.Infrastructure.Services;

namespace SquashLeagueService.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAuthenticationService, AuthenticationService>();

        return services;
    }
}