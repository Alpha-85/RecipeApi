
using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models.SpoonResponse;

[Serializable]
public class Equipment
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("localizedName")]
    public string LocalizedName { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; }
}

