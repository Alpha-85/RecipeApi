using MediatR;
using RecipeApi.Application.Common.Models.Recipes.RecipeResponse;

namespace RecipeApi.Application.Recipes.Queries.GetRecipeIngredients;

public class GetShoppingIngredientsQuery : IRequest<RecipeShoppingListResponse>
{
    public int UserId { get; }
    public int CollectionId { get; }

    public GetShoppingIngredientsQuery(in int userId, in int collectionId)
    {
        UserId = userId;
        CollectionId = collectionId;
    }
}
