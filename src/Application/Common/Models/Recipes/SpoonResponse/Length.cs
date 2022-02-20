
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models.SpoonResponse;
[ExcludeFromCodeCoverage]
[Serializable]
public class Length
{
    [JsonPropertyName("number")]
    public int Number { get; set; }

    [JsonPropertyName("unit")]
    public string Unit { get; set; }
}
