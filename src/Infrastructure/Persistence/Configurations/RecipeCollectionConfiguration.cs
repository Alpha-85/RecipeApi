using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeApi.Domain.Entities;

namespace RecipeApi.Infrastructure.Persistence.Configurations;

public class RecipeCollectionConfiguration : IEntityTypeConfiguration<RecipeCollection>
{
    public void Configure(EntityTypeBuilder<RecipeCollection> builder)
    {

    }
}
