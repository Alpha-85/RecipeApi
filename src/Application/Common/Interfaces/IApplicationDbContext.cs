using Microsoft.EntityFrameworkCore;
using RecipeApi.Domain.Entities;

namespace RecipeApi.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<RecipeCollection> RecipeCollections { get; }
    DbSet<RecipeDay> RecipeDays { get; }
    DbSet<RecipeInformation> RecipeInformation { get; }
    DbSet<RefreshToken> RefreshTokens { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
