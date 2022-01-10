using Microsoft.Extensions.Caching.Memory;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models.SpoonResponse;
using RecipeApi.Domain.Enums;

namespace RecipeApi.Application.Services;

public class MemoryCacheService : IMemoryCacheService
{
    private readonly IMemoryCache _memory;
    private readonly ISpoonAdapter _spoonAdapter;
    public MemoryCacheService(IMemoryCache memory, ISpoonAdapter spoonAdapter)
    {
        _memory = memory ?? throw new ArgumentNullException(nameof(memory));
        _spoonAdapter = spoonAdapter ?? throw new ArgumentNullException(nameof(spoonAdapter));
    }



    public async Task<List<Recipe>> GetCachedRecipes(IngredientType mainIngredient, string query)
    {
        List<Recipe> output;

        output = _memory.Get<List<Recipe>>(mainIngredient);

        if (output is null)
        {
            output = new();
            var response = await _spoonAdapter.GetRandomRecipesAsync(query);

            output.AddRange(response);

            _memory.Set(mainIngredient, output, TimeSpan.FromHours(1));
        }

        return output;
    }

}
