
using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models.SpoonResponse;

[Serializable]
public class Recipe
{
    [JsonPropertyName("vegetarian")]
    public bool Vegetarian { get; set; }

    [JsonPropertyName("vegan")]
    public bool Vegan { get; set; }

    [JsonPropertyName("glutenFree")]
    public bool GlutenFree { get; set; }

    [JsonPropertyName("dairyFree")]
    public bool DairyFree { get; set; }

    [JsonPropertyName("veryHealthy")]
    public bool VeryHealthy { get; set; }

    [JsonPropertyName("cheap")]
    public bool Cheap { get; set; }

    [JsonPropertyName("veryPopular")]
    public bool VeryPopular { get; set; }

    [JsonPropertyName("sustainable")]
    public bool Sustainable { get; set; }

    [JsonPropertyName("weightWatcherSmartPoints")]
    public int WeightWatcherSmartPoints { get; set; }

    [JsonPropertyName("gaps")]
    public string Gaps { get; set; }

    [JsonPropertyName("lowFodmap")]
    public bool LowFodmap { get; set; }

    [JsonPropertyName("aggregateLikes")]
    public int AggregateLikes { get; set; }

    [JsonPropertyName("spoonacularScore")]
    public double SpoonacularScore { get; set; }

    [JsonPropertyName("healthScore")]
    public double HealthScore { get; set; }

    [JsonPropertyName("creditsText")]
    public string CreditsText { get; set; }

    [JsonPropertyName("license")]
    public string License { get; set; }

    [JsonPropertyName("sourceName")]
    public string SourceName { get; set; }

    [JsonPropertyName("pricePerServing")]
    public double PricePerServing { get; set; }

    [JsonPropertyName("extendedIngredients")]
    public List<ExtendedIngredient> ExtendedIngredients { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

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

    [JsonPropertyName("summary")]
    public string Summary { get; set; }

    [JsonPropertyName("cuisines")]
    public List<string> Cuisines { get; set; }

    [JsonPropertyName("dishTypes")]
    public List<string> DishTypes { get; set; }

    [JsonPropertyName("diets")]
    public List<string> Diets { get; set; }

    [JsonPropertyName("occasions")]
    public List<object> Occasions { get; set; }

    [JsonPropertyName("instructions")]
    public string Instructions { get; set; }

    [JsonPropertyName("analyzedInstructions")]
    public List<AnalyzedInstruction> AnalyzedInstructions { get; set; }

    [JsonPropertyName("originalId")]
    public object OriginalId { get; set; }

    [JsonPropertyName("spoonacularSourceUrl")]
    public string SpoonacularSourceUrl { get; set; }
}