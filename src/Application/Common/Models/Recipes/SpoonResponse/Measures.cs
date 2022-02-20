
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models.SpoonResponse;
[ExcludeFromCodeCoverage]
[Serializable]
public class Measures
{
    [JsonPropertyName("us")]
    public Us Us { get; set; }

    [JsonPropertyName("metric")]
    public Metric Metric { get; set; }
}
[ExcludeFromCodeCoverage]
public class Us
{
    [JsonPropertyName("amount")]
    public double Amount { get; set; }

    [JsonPropertyName("unitShort")]
    public string UnitShort { get; set; }

    [JsonPropertyName("unitLong")]
    public string UnitLong { get; set; }
}
[ExcludeFromCodeCoverage]
public class Metric
{
    [JsonPropertyName("amount")]
    public double Amount { get; set; }

    [JsonPropertyName("unitShort")]
    public string UnitShort { get; set; }

    [JsonPropertyName("unitLong")]
    public string UnitLong { get; set; }
}
