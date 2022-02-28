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
        var mealType = request.UserRecipe.RecipeInformation.MealType;
        var day = request.UserRecipe.WeekdayId;
        var collectionId = request.UserRecipe.CollectionId;

        var recipeDay = _context.RecipeDays
            .Include(r => r.Recipes)
            .Where(m => m.WeekdayId == day && m.RecipeCollectionId == collectionId)
            .Select(r => r)
            .FirstOrDefault() ?? await AddRecipeDay(request, cancellationToken);

        if (recipeDay.Recipes != null && recipeDay.Recipes.Any(r => r.MealType == mealType)) return false;

        await AddRecipe(recipeDay, request, cancellationToken);

        return true;
    }

    private async Task AddRecipe(RecipeDay recipeDay, AddRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = _mapper.Map<RecipeInformation>(request.UserRecipe.RecipeInformation);
        recipe.RecipeDayId = recipeDay.Id;

        await _context.RecipeInformation.AddAsync(recipe, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<RecipeDay> AddRecipeDay(AddRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipeDay = new RecipeDay()
        {
            RecipeCollectionId = request.UserRecipe.CollectionId,
            WeekdayId = request.UserRecipe.WeekdayId,
        };

        await _context.RecipeDays.AddAsync(recipeDay, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return recipeDay;
    }
}
