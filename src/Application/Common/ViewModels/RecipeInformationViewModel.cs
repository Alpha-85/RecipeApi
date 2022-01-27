using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models;

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
}
