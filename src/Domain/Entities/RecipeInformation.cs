
namespace RecipeApi.Domain.Entities;

public class RecipeInformation : AuditableEntity
{
    public int Id { get; set; }
    public int RecipeDayId { get; set; }
    public RecipeDay RecipeDay { get; set; }
    public long RecipeId { get; set; }
    public string RecipeName { get; set; }
    public string RecipeUrl { get; set; }
    public int ReadyInMinutes { get; set; }
    public string Image { get; set; }
    public MealType MealType { get; set; }

}
