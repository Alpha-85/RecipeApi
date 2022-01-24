
using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models.Recipes;

public class Allergies
{
    [JsonPropertyName("glutenFree")]
    public bool IsGlutenFree { get; set; }
    [JsonPropertyName("dairyFree")]
    public bool IsDairyFree { get; set; }
    [JsonPropertyName("otherAllergies")]
    public string OtherAllergies { get; set; }
}
