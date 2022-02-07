using Microsoft.EntityFrameworkCore;
using RecipeApi.Infrastructure.Persistence;
using System;

namespace Application.IntegrationTests.Helpers;

public static class DbContextHelper
{
    public static ApplicationDbContext GetApplicationDbContext()
    {
        DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        ApplicationDbContext applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);
        return applicationDbContext;
    }
}
