using System.Text.Json.Serialization;
using RecipeApi.Domain.Enums;

namespace RecipeApi.Application.Common.Models;
[Serializable]
public class RecipeDetailsViewModel
{
    [JsonPropertyName("recipeId")]
    public long RecipeId { get; set; }
    [JsonPropertyName("recipeName")]
    public string RecipeName { get; set; }
    [JsonPropertyName("recipeUrl")]
    public string RecipeUrl { get; set; }
    [JsonPropertyName("readyInMinutes")]
    public int ReadyInMinutes { get; set; }
    [JsonPropertyName("image")]
    public string Image { get; set; }
    [JsonPropertyName("mealType")]
    public MealType MealType { get; set; }
}
