
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RecipeApi.Application.Adapters;
using RecipeApi.Application.Authorization;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Settings;
using RecipeApi.Application.Services;
using System.Reflection;

namespace RecipeApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped<SpoonApiSettings>();
        services.AddScoped<IMemoryCacheService, MemoryCacheService>();
        services.AddTransient<ISpoonAdapter, SpoonAdapter>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserService, UserService>();
        services.AddTransient<IRecipeFilteredService,RecipeFilteredService>();

        return services;
    }
}

