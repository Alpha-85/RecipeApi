using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Domain.Entities;

namespace RecipeApi.Application.Recipes.Commands;

public class AddRecipeCommandHandler : IRequestHandler<AddRecipeCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public AddRecipeCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(AddRecipeCommand request, CancellationToken cancellationToken)
    {
        var user = _context.Users
            .Include(x => x.RecipeCollections)
            .SingleOrDefault(user => user.Id == request.UserRecipe.UserId);

        if (user is null)
            return false;

        var isExisting = _context.RecipeInformation
            .Select(x => x.RecipeName == request.UserRecipe.RecipeInformation.RecipeName)
            .FirstOrDefault();

        if (!isExisting)
        {
            var recipe = _mapper.Map<RecipeInformation>(request.UserRecipe.RecipeInformation);
            await _context.RecipeInformation.AddAsync(recipe, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        
        var recipeInfo = await _context.RecipeInformation
            .Where(r => r.RecipeName == request.UserRecipe.RecipeInformation.RecipeName)
            .FirstOrDefaultAsync(CancellationToken.None);

        if (recipeInfo != null)
        {
            var recipeDay = new RecipeDay()
            {
                RecipeCollectionId = request.UserRecipe.CollectionId,
                RecipeId = recipeInfo.Id,
                WeekdayId = request.UserRecipe.WeekdayId,
            };

            await _context.RecipeDays.AddAsync(recipeDay, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);


        return true;
    }
}
