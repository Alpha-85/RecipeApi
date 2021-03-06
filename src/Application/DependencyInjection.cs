
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RecipeApi.Application.Common.Settings;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using RecipeApi.Application.Common.Extensions;
using RecipeApi.Application.Common.Interfaces;

namespace RecipeApi.Application;
[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped<SpoonApiSettings>();
        services.AddScoped<IIpAddressExtensions, IpAddressExtensions>();
        return services;
    }
}

