using RecipeApi.Domain.Enums;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models.Recipes;
[ExcludeFromCodeCoverage]
[Serializable]
public class RecipeRequest
{
    [JsonPropertyName("mealType")]
    public MealType MealType { get; set; }
    [JsonPropertyName("preference")]
    public PreferenceType Preference { get; set; }
    [JsonPropertyName("allergies")]
    public AllergiesViewModel Allergies { get; set; }
}
