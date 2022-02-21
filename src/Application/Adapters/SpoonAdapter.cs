using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models.SpoonResponse;
using RecipeApi.Application.Common.Settings;

namespace RecipeApi.Application.Adapters;

public class SpoonAdapter : ISpoonAdapter
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SpoonAdapter> _logger;
    private readonly SpoonApiSettings _spoonApiSettings;

    public SpoonAdapter(HttpClient httpClient, ILogger<SpoonAdapter> logger, IOptions<SpoonApiSettings> options)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _spoonApiSettings = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

   

    public async Task<List<Recipe>> GetRandomRecipesAsync(string query)
    {
        List<Recipe> list = new();

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,
            $"https://api.spoonacular.com/recipes/random?limitLicense=true&tags={query}&number=100&apiKey={_spoonApiSettings.ApiKey}");
        httpRequestMessage.Headers.Add("Accept", "application/json");

        var response = await _httpClient.SendAsync(httpRequestMessage);

        if (response.IsSuccessStatusCode)
        {
            var json = await response?.Content.ReadAsStringAsync();
            _logger.LogInformation(json);
            var obj = JsonSerializer.Deserialize<RecipeList>(json);

            if (obj is not null)
            {
                list.AddRange(obj.Recipes);
            }
        }


        return list;

    }

    public async Task<List<ExtendedIngredient>> GetRecipeIngredientsAsync(long recipeId)
    {

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,
            $"https://api.spoonacular.com/recipes/{recipeId}/information?includeNutrition=false&apiKey={_spoonApiSettings.ApiKey}");
        httpRequestMessage.Headers.Add("Accept", "application/json");

        var response = await _httpClient.SendAsync(httpRequestMessage);

        if (!response.IsSuccessStatusCode) return new List<ExtendedIngredient>();
        var json = await response?.Content.ReadAsStringAsync();
        _logger.LogInformation(json);

        var obj = JsonSerializer.Deserialize<Recipe>(json);

        return obj is not null ? obj.ExtendedIngredients : new List<ExtendedIngredient>();
    }
}

