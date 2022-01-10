using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeApi.Domain.Entities;

namespace RecipeApi.Infrastructure.Persistence.Configurations;

public class RecipeCollectionConfiguration : IEntityTypeConfiguration<RecipeCollection>
{
    public void Configure(EntityTypeBuilder<RecipeCollection> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.RecipeDays)
            .WithOne(x => x.RecipeCollection)
            .HasForeignKey(x => x.RecipeCollectionId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(x => x.CollectionName)
            .HasMaxLength(50);
    }
}
