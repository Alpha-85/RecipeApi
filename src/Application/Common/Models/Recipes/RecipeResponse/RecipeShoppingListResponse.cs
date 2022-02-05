using RecipeApi.Application.Common.ViewModels;

namespace RecipeApi.Application.Common.Models.Recipes.RecipeResponse;

public class RecipeShoppingListResponse
{
    public List<ShoppingIngredientViewModel> ShoppingIngredients { get; set; }
}
