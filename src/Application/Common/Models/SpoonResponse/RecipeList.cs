using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models.SpoonResponse;

[Serializable]
public class RecipeList
{
    [JsonPropertyName("recipes")]
    public List<Recipe> Recipes { get; set; }
}
