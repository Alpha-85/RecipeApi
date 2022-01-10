namespace RecipeApi.Domain.Entities;

public class RecipeDay : AuditableEntity
{
    public int Id { get; set; }
    public int RecipeCollectionId { get; set; }
    public RecipeCollection RecipeCollection { get; set; }
    public int WeekdayId { get; set; }
    public WeekDay Weekday { get; set; }
    public int RecipeId { get; set; }
    public RecipeInformation Recipe { get; set; }
}
