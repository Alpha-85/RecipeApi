using RecipeApi.Application.Common.Models.SpoonResponse;
using RecipeApi.Domain.Enums;


namespace RecipeApi.Application.Common.Interfaces;

public interface IMemoryCachedRecipes
{
    Task<RecipeList> GetCachedRecipes(IngredientType mainIngredient, string query);
}
