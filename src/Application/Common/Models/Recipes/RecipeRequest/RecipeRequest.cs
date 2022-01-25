using RecipeApi.Domain.Enums;
using System.Text.Json.Serialization;
using RecipeApi.Application.Common.ViewModels;

namespace RecipeApi.Application.Common.Models.Recipes;

public class RecipeRequest
{
    [JsonPropertyName("mealType")]
    public MealType MealType { get; set; }
    [JsonPropertyName("preference")]
    public PreferenceType Preference { get; set; }
    [JsonPropertyName("allergies")]
    public AllergiesViewModel Allergies { get; set; }
}
