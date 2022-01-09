using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeApi.Domain.Entities;

namespace RecipeApi.Infrastructure.Persistence.Configurations;

public class RecipeInformationConfiguration : IEntityTypeConfiguration<RecipeInformation>
{
    public void Configure(EntityTypeBuilder<RecipeInformation> builder)
    {
        // builder.HasMany(x => x.RecipeCollections).WithMany(x => x.RecipeDays);

    }
}
