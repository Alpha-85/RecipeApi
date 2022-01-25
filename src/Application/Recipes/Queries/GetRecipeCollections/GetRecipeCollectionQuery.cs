using MediatR;
using RecipeApi.Application.Common.Models;

namespace RecipeApi.Application.Recipes.Queries.GetRecipeCollections;

public class GetRecipeCollectionQuery : IRequest<List<RecipeCollectionResponse>>
{
    public int Id { get; }

    public GetRecipeCollectionQuery(in int id)
    {
        Id = id;
    }
}
