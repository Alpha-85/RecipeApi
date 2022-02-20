using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using RecipeApi.Domain.Enums;

namespace RecipeApi.Application.Common.Models;
[ExcludeFromCodeCoverage]
public class RecipeInformationViewModel
{
    [JsonPropertyName("recipeName")]
    public string RecipeName { get; set; }
    [JsonPropertyName("recipeId")]
    public long RecipeId { get; set; }
    [JsonPropertyName("readyInMinutes")]
    public int ReadyInMinutes { get; set; }
    [JsonPropertyName("sourceUrl")]
    public string RecipeUrl { get; set; }
    [JsonPropertyName("image")]
    public string Image { get; set; }
    [JsonPropertyName("mealType")] 
    public MealType MealType { get; set; }
}
