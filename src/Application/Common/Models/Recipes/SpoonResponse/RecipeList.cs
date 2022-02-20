using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models.SpoonResponse;
[ExcludeFromCodeCoverage]
[Serializable]
public class RecipeList
{
    [JsonPropertyName("recipes")]
    public List<Recipe> Recipes { get; set; }
}
