namespace RecipeApi.Domain.Entities;

public class RecipeCollection : AuditableEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public string CollectionName { get; set; }
    public List<RecipeDay> RecipeDays { get; set; }

}
