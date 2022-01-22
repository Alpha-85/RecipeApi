using RecipeApi.Application.Common.Models.SpoonResponse;
using RecipeApi.Domain.Enums;


namespace RecipeApi.Application.Common.Interfaces;

public interface IMemoryCacheService
{
    Task<List<Recipe>> GetCachedRecipes(PreferenceType mainIngredient, string query);
}
