using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models.SpoonResponse;
using RecipeApi.Application.Common.Settings;

namespace RecipeApi.Application.Adapters;

public class SpoonAdapter : ISpoonAdapter
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SpoonAdapter> _logger;
    private readonly SpoonApiSettings _spoonApiSettings;

    public SpoonAdapter(HttpClient httpClient, ILogger<SpoonAdapter> logger, SpoonApiSettings settings, IOptions<SpoonApiSettings> options)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _spoonApiSettings = settings ?? throw new ArgumentNullException(nameof(settings));
        _spoonApiSettings = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<List<Recipe>> GetRandomRecipesAsync(string query)
    {
        List<Recipe> list = new();

        var url = $"{_spoonApiSettings.BaseUrl}/random?limitLicense=true&tags={query}&number=1&apiKey={_spoonApiSettings.ApiKey}";
        // /recipes/random?limitLicense=true&tags=beef&number=10&apiKey=b87efff8b00e4f22a2f3348ca1b13acb
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"/recipes/random?limitLicense=true&tags={query}&number=1&apiKey={_spoonApiSettings.ApiKey}");
        httpRequestMessage.Headers.Add("Accept", "application/json");

        var response = await _httpClient.SendAsync(httpRequestMessage);

        if (response.IsSuccessStatusCode)
        {
            var json = await response?.Content.ReadAsStringAsync();
            _logger.LogInformation(json);

            var obj = JsonConvert.DeserializeObject<RecipeList>(json);

            if (obj is not null)
            {
                list.AddRange(obj.Recipes);
            }
        }


        return list;

    }
}

