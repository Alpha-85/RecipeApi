
using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models.SpoonResponse;

[Serializable]
public class ExtendedIngredient
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("aisle")]
    public string Aisle { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; }

    [JsonPropertyName("consistency")]
    public string Consistency { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("nameClean")]
    public string NameClean { get; set; }

    [JsonPropertyName("original")]
    public string Original { get; set; }

    [JsonPropertyName("originalString")]
    public string OriginalString { get; set; }

    [JsonPropertyName("originalName")]
    public string OriginalName { get; set; }

    [JsonPropertyName("amount")]
    public double Amount { get; set; }

    [JsonPropertyName("unit")]
    public string Unit { get; set; }

    [JsonPropertyName("meta")]
    public List<string> Meta { get; set; }

    [JsonPropertyName("metaInformation")]
    public List<string> MetaInformation { get; set; }

    [JsonPropertyName("measures")]
    public Measures Measures { get; set; }
}
