using MediatR;
using RecipeApi.Application.Common.Models.Recipes.RecipeResponse;

namespace RecipeApi.Application.Recipes.Queries.GetRecipeIngredients;

internal class GetShoppingIngredientsHandler : IRequestHandler<GetShoppingIngredientsQuery, RecipeShoppingListResponse>
{
    public Task<RecipeShoppingListResponse> Handle(GetShoppingIngredientsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
