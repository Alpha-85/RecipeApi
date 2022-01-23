
namespace RecipeApi.Application.Common.Models.Recipes;

public class Allergies
{
    public bool IsGlutenFree { get; set; }
    public bool IsDairyFree { get; set; }
    public string OtherAllergies { get; set; }
}
