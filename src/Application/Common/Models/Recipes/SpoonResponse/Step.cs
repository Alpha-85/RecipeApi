
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models.SpoonResponse;
[ExcludeFromCodeCoverage]
[Serializable]
public class Step
{
    [JsonPropertyName("number")]
    public int Number { get; set; }

    [JsonPropertyName("step")]
    public string Steps { get; set; }

    [JsonPropertyName("ingredients")]
    public List<Ingredient> Ingredients { get; set; }

    [JsonPropertyName("equipment")]
    public List<Equipment> Equipment { get; set; }

    [JsonPropertyName("length")]
    public Length Length { get; set; }
}
