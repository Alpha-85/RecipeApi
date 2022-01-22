using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models.SpoonResponse;

namespace RecipeApi.Application.Services;

public class RecipeFilteredService : IRecipeFilteredService
{
    public Task<List<Recipe>> GetThreeRandomRecipes(List<Recipe> listToFilter, string allergies)
    {
        throw new NotImplementedException();
    }
}
