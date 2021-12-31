using RecipeApi.Application.Common.Models.SpoonResponse;

namespace RecipeApi.Application.Common.Interfaces;

public interface ISpoonAdapter
{
    Task<RecipeList> GetRandomRecipesAsync(string query);
}
