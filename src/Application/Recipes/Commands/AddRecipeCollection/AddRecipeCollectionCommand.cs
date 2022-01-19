
using MediatR;
using RecipeApi.Application.Common.Models;

namespace RecipeApi.Application.Recipes.Commands.AddRecipeCollection;

public class AddRecipeCollectionCommand : IRequest<RecipeCollectionViewModel>
{
    public int UserId { get; set; }
    public string CollectionName { get; }

    public AddRecipeCollectionCommand(in string collectionName, in int userId)
    {
        UserId = userId;
        CollectionName = collectionName;
    }
}
