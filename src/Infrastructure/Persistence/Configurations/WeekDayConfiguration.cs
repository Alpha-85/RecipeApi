using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeApi.Domain.Entities;

namespace RecipeApi.Infrastructure.Persistence.Configurations;

public class WeekDayConfiguration : IEntityTypeConfiguration<WeekDay>
{
    public void Configure(EntityTypeBuilder<WeekDay> builder)
    {
        builder.Property(x => x.DayOfWeek).HasMaxLength(7);
    }
}