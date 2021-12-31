using RecipeApi.Application.Common.Models.SpoonResponse;
using RecipeApi.Domain.Enums;


namespace RecipeApi.Application.Common.Interfaces;

public interface IMemoryCachedRecipes
{
    Task<List<Recipe>> GetCachedRecipes(IngredientType mainIngredient, string query);
}
