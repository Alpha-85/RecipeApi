using RecipeApi.Domain.Enums;

namespace RecipeApi.Application.Common.Models.Recipes;

public class RecipeRequest
{
    public MealType MealType { get; set; }
    public PreferenceType Preference { get; set; }
    public Allergies Allergies { get; set; }
}
