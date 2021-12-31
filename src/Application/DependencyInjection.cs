
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RecipeApi.Application.Adapters;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Settings;
using RecipeApi.Application.Services;
using System.Reflection;

namespace RecipeApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped<SpoonApiSettings>();
        services.AddScoped<IMemoryCachedRecipes, MemoryCachedRecipes>();
        services.AddTransient<ISpoonAdapter, SpoonAdapter>();

        return services;
    }
}

