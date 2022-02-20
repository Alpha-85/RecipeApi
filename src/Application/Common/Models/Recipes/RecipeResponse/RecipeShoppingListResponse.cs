using System.Diagnostics.CodeAnalysis;
using RecipeApi.Application.Common.ViewModels;

namespace RecipeApi.Application.Common.Models.Recipes.RecipeResponse;
[ExcludeFromCodeCoverage]
public class RecipeShoppingListResponse
{
    public List<ShoppingIngredientViewModel> ShoppingIngredients { get; set; } = new();
}
