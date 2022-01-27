using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Domain.Entities;
using RecipeApi.Domain.Enums;

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

        var mealType = request.UserRecipe.MealType;
        var day = request.UserRecipe.WeekdayId;

        var isMatch = FindMatch(request, mealType, day);

        if (isMatch) return false;

        await AddRecipe(request, cancellationToken);

        return true;
    }

    private bool FindMatch(AddRecipeCommand request, MealType mealType, int day)
    {
        var match = _context.RecipeDays
            .Where(m => m.MealType == mealType && m.WeekdayId == day &&
                        m.RecipeCollectionId == request.UserRecipe.CollectionId)
            .Select(r => r)
            .FirstOrDefault();

        return match != null;
    }

    private async Task AddRecipe(AddRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = _mapper.Map<RecipeInformation>(request.UserRecipe.RecipeInformation);
        await _context.RecipeInformation.AddAsync(recipe, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var recipeDay = new RecipeDay()
        {
            RecipeCollectionId = request.UserRecipe.CollectionId,
            RecipeInformationId = recipe.Id,
            WeekdayId = request.UserRecipe.WeekdayId,
            MealType = request.UserRecipe.MealType
        };

        await _context.RecipeDays.AddAsync(recipeDay, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
