
namespace RecipeApi.Domain.Entities;

public class RecipeInformation : AuditableEntity
{
    public int Id { get; set; }
    public long RecipeId { get; set; }
    public string RecipeName { get; set; }
    public string RecipeUrl { get; set; }
    public int ReadyInMinutes { get; set; }
    public string Image { get; set; }

}
