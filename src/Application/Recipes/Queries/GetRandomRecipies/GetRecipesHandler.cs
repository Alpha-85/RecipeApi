using AutoMapper;
using MediatR;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models;
using RecipeApi.Domain.Enums;

namespace RecipeApi.Application.Recipes.Queries.GetRandomRecipies;

public class GetRecipesHandler : IRequestHandler<GetRecipesQuery, List<RecipeViewModel>>
{
    private readonly IMapper _mapper;
    private readonly IMemoryCacheService _memoryCachedRecipe;

    public GetRecipesHandler(IMemoryCacheService memoryCachedRecipe, IMapper mapper)
    {
        _memoryCachedRecipe = memoryCachedRecipe ?? throw new ArgumentNullException(nameof(memoryCachedRecipe));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<RecipeViewModel>> Handle(GetRecipesQuery query, CancellationToken cancellationToken)
    {
        var isValidEnum = Enum.TryParse(
            query.MainIngredient,
            true,
            out IngredientType parsedIngredient)
            && Enum.IsDefined(typeof(IngredientType), parsedIngredient);

        if (isValidEnum is false) return new List<RecipeViewModel>();


        var collectedQuery = string.Join(",", query.MainIngredient, query.MealType);

        var content = await _memoryCachedRecipe.GetCachedRecipes(parsedIngredient, collectedQuery);

        var result = new List<RecipeViewModel>();

        result = content.Select(recipe => _mapper.Map<RecipeViewModel>(recipe)).ToList(); 

        return result;
    }
}
