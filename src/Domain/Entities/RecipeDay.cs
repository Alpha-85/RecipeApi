using System.Diagnostics.CodeAnalysis;

namespace RecipeApi.Domain.Entities;
[ExcludeFromCodeCoverage]
public class RecipeDay : AuditableEntity
{
    public int Id { get; set; }
    public int RecipeCollectionId { get; set; }
    public RecipeCollection RecipeCollection { get; set; }
    public int WeekdayId { get; set; }
    public WeekDay Weekday { get; set; }
    public List<RecipeInformation> Recipes { get; set; }

}
