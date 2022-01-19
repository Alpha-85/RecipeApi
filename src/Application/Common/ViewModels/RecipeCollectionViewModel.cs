using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models;

public class RecipeCollectionViewModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("collectionName")]
    public string CollectionName { get; set; }
}
