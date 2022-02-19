
using RecipeApi.Application.Common.Models.SpoonResponse;
using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models;

public class RecipeViewModel
{
    [JsonPropertyName("vegetarian")]
    public bool Vegetarian { get; set; }
    [JsonPropertyName("vegan")]
    public bool Vegan { get; set; }
    [JsonPropertyName("glutenFree")]
    public bool GlutenFree { get; set; }
    [JsonPropertyName("dairyFree")]
    public bool DairyFree { get; set; }
    [JsonPropertyName("aggregateLikes")]
    public int AggregateLikes { get; set; }
    [JsonPropertyName("spoonacularScore")]
    public int SpoonAcularScore { get; set; }
    [JsonPropertyName("extendedIngredients")]
    public List<ExtendedIngredient> ExtendedIngredients { get; set; }
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("readyInMinutes")]
    public int ReadyInMinutes { get; set; }
    [JsonPropertyName("servings")]
    public int Servings { get; set; }
    [JsonPropertyName("sourceUrl")]
    public string SourceUrl { get; set; }
    [JsonPropertyName("image")]
    public string Image { get; set; }
    [JsonPropertyName("imageType")]
    public string ImageType { get; set; }
    [JsonPropertyName("diets")]
    public List<string> Diets { get; set; }
    [JsonPropertyName("instructions")]
    public List<string> Instructions { get; set; }
    [JsonPropertyName("spoonacularSourceUrl")]
    public string SpoonacularSourceUrl { get; set; }
}
