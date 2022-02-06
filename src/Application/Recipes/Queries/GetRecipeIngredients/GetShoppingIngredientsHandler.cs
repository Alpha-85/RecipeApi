using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models.Recipes.RecipeResponse;
using RecipeApi.Application.Common.Models.SpoonResponse;
using RecipeApi.Application.Common.ViewModels;

namespace RecipeApi.Application.Recipes.Queries.GetRecipeIngredients;

public class GetShoppingIngredientsHandler : IRequestHandler<GetShoppingIngredientsQuery, RecipeShoppingListResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISpoonAdapter _spoonAdapter;

    public GetShoppingIngredientsHandler(IApplicationDbContext context, IMapper mapper, ISpoonAdapter spoonAdapter)
    {
        _context = context;
        _mapper = mapper;
        _spoonAdapter = spoonAdapter;
    }

    public async Task<RecipeShoppingListResponse> Handle(GetShoppingIngredientsQuery request, CancellationToken cancellationToken)
    {
        var recipes = await _context.RecipeCollections
            .Where(u => u.UserId == request.UserId && u.Id == request.CollectionId)
            .Include(r => r.RecipeDays)
            .ThenInclude(x => x.Recipes)
            .Include(x => x.RecipeDays)
            .ThenInclude(x => x.Weekday)
            .SelectMany(x => x.RecipeDays)
            .SelectMany(x => x.Recipes)
            .ToListAsync(cancellationToken);

        var list = new List<ExtendedIngredient>();

        foreach (var day in recipes)
        {
            list.AddRange(await _spoonAdapter.GetRecipeIngredientsAsync(day.RecipeId));
        }

       
        var ingredientView = list.GroupBy(x => x.Name).Select(y => new ShoppingIngredientViewModel()
        {
            Name = y.Key,
            Amount = y.Sum(x => x.Amount)

        }).ToList();

        var response = new RecipeShoppingListResponse();
        response.ShoppingIngredients.AddRange(ingredientView);

        return response;
    }
}
