using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models;

public class RecipeCollectionResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("collectionName")]
    public string CollectionName { get; set; }
}
