
using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models.SpoonResponse;

[Serializable]
public class Length
{
    [JsonPropertyName("number")]
    public int Number { get; set; }

    [JsonPropertyName("unit")]
    public string Unit { get; set; }
}
