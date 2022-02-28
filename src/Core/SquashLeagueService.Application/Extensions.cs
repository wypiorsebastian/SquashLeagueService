﻿using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace SquashLeagueService.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}