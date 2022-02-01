using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeApi.Domain.Entities;

namespace RecipeApi.Infrastructure.Persistence.Configurations;

public class RecipeDayConfiguration : IEntityTypeConfiguration<RecipeDay>
{
    public void Configure(EntityTypeBuilder<RecipeDay> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(r => r.Recipes)
            .WithOne(d => d.RecipeDay)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
