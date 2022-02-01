using MediatR;
using Microsoft.EntityFrameworkCore;
using RecipeApi.Application.Common.Interfaces;

namespace RecipeApi.Application.Recipes.Commands.DeleteRecipeCollection;

public class DeleteRecipeCollectionCommandHandler : IRequestHandler<DeleteRecipeCollectionCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteRecipeCollectionCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<bool> Handle(DeleteRecipeCollectionCommand request, CancellationToken cancellationToken)
    {
        var collection = await _context.RecipeCollections
            .Include(r => r.RecipeDays)
            .Where(c => c.UserId == request.UserId && c.Id == request.CollectionId)
            .FirstOrDefaultAsync(cancellationToken);

        if (collection is null)
            return false;

        _context.RecipeCollections.Remove(collection);
        await _context.SaveChangesAsync(cancellationToken);

        return true;


    }
}
