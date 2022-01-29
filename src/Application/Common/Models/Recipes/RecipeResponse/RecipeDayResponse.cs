using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models;
[Serializable]
public class RecipeDayResponse
{
    [JsonPropertyName("weekday")]
    public string Weekday { get; set; }
    [JsonPropertyName("recipe")]
    public RecipeDetailsViewModel Recipe { get; set; }
}
