
using RecipeApi.Application.Common.Models.SpoonResponse;

namespace RecipeApi.Application.Common.Interfaces;

public interface IRecipeFilteredService
{
    Task<List<Recipe>> GetThreeRandomRecipes(List<Recipe> listToFilter, string allergies);
}
