using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeApi.Domain.Entities;

namespace RecipeApi.Infrastructure.Persistence.Configurations;

public class RecipeInformationConfiguration : IEntityTypeConfiguration<RecipeInformation>
{
    public void Configure(EntityTypeBuilder<RecipeInformation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.RecipeId);
       
        builder.Property(x => x.RecipeUrl)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(x => x.RecipeName)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(x => x.RecipeId)
            .IsRequired();
        builder.Property(x => x.ReadyInMinutes)
            .IsRequired();
        builder.Property(x => x.Image)
            .IsRequired();

    }
}
