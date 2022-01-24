using RecipeApi.Domain.Enums;
using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models.Recipes;

public class RecipeRequest
{
    [JsonPropertyName("mealType")]
    public MealType MealType { get; set; }
    [JsonPropertyName("preference")]
    public PreferenceType Preference { get; set; }
    [JsonPropertyName("allergies")]
    public Allergies Allergies { get; set; }
}
