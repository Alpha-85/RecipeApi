using MediatR;
using RecipeApi.Application.Common.Models;

namespace RecipeApi.Application.Recipes.Queries.GetRecipe;
public class GetRecipeQuery : IRequest<RecipeViewModel>
{
    public long RecipeId { get; set; }

    public GetRecipeQuery(in long recipeId)
    {
        RecipeId = recipeId;
    }
}
