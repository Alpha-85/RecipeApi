using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Infrastructure.Adapters;
using RecipeApi.Infrastructure.Persistence;
using RecipeApi.Infrastructure.Services;

namespace RecipeApi.Infrastructure;
[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if(configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            });
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("HGDatabase"));
            });
        }
       
        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        services.AddTransient<ISpoonAdapter, SpoonAdapter>();
        services.AddScoped<IMemoryCacheService, MemoryCacheService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserService, UserService>();
        services.AddHttpClient<ISpoonAdapter, SpoonAdapter>(c =>
        {
            // c.BaseAddress = new Uri("https://api.spoonacular.com/");
            c.DefaultRequestHeaders.Add("Accept", "application/.json");
        });

        return services;
    }
}

