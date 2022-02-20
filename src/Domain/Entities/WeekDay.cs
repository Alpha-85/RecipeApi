using System.Diagnostics.CodeAnalysis;

namespace RecipeApi.Domain.Entities;
[ExcludeFromCodeCoverage]
public class WeekDay
{
    public int Id { get; set; }
    public string DayOfWeek { get; set; }
}
