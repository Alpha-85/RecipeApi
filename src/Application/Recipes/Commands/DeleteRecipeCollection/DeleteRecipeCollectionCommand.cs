using MediatR;

namespace RecipeApi.Application.Recipes.Commands.DeleteRecipeCollection;

public class DeleteRecipeCollectionCommand : IRequest<bool>
{
    public int UserId { get; }
    public int CollectionId { get; }

    public DeleteRecipeCollectionCommand(in int userId, in int collectionId)
    {
        UserId = userId;
        CollectionId = collectionId;
    }
}
