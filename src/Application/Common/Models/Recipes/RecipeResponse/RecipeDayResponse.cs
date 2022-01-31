using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models;
[Serializable]
public class RecipeDayResponse
{
    [JsonPropertyName("weekday")]
    public string Weekday { get; set; }
    [JsonPropertyName("recipe")]
    public List<RecipeInformationViewModel> RecipesList { get; set; }
}
