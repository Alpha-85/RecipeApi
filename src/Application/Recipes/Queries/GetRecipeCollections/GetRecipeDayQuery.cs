using MediatR;
using RecipeApi.Application.Common.Models;

namespace RecipeApi.Application.Recipes.Queries.GetRecipeCollections;

public class GetRecipeDayQuery : IRequest<List<RecipeDayResponse>>
{
    public int UserId { get; }
    public int CollectionId { get; }

    public GetRecipeDayQuery(in int userId, in int collectionId)
    {
        UserId = userId;
        CollectionId = collectionId;
    }
}
