using MediatR;
using RecipeApi.Application.Common.Models;
using RecipeApi.Application.Common.Models.Recipes;
using RecipeApi.Domain.Enums;

namespace RecipeApi.Application.Recipes.Queries.GetRandomRecipes;

public class GetRecipesQuery : IRequest<List<RecipeViewModel>>
{
    public RecipeRequest Request { get; }

    public GetRecipesQuery(in RecipeRequest request)
    {
        Request = request;
    }
}
