using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models.UserRecipes;

public class UserRecipeRequest
{
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
    [JsonPropertyName("collectionId")]
    public int CollectionId { get; set; }
    [JsonPropertyName("weekdayId")]
    public int WeekdayId { get; set; }
    public RecipeInformationViewModel RecipeInformation { get; set; }

}
