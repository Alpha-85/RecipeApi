
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models.SpoonResponse;
[ExcludeFromCodeCoverage]
[Serializable]
public class AnalyzedInstruction
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("steps")]
    public List<Step> Steps { get; set; }
}

