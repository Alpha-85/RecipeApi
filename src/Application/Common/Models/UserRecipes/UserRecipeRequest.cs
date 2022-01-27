using System.Text.Json.Serialization;
using RecipeApi.Domain.Enums;

namespace RecipeApi.Application.Common.Models.UserRecipes;

public class UserRecipeRequest
{
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
    [JsonPropertyName("collectionId")]
    public int CollectionId { get; set; }
    [JsonPropertyName("weekdayId")]
    public int WeekdayId { get; set; }
    [JsonPropertyName("mealType")]
    public MealType MealType { get; set; }
    public RecipeInformationViewModel RecipeInformation { get; set; }

}
