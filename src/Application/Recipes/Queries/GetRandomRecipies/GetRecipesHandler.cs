using MediatR;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models.SpoonResponse;
using RecipeApi.Domain.Enums;

namespace RecipeApi.Application.Recipes.Queries.GetRandomRecipies;

public class GetRecipesHandler : IRequestHandler<GetRecipesQuery, List<Recipe>>
{
    //private readonly IMapper _mapper;
    private readonly IMemoryCacheService _memoryCachedRecipe;

    public GetRecipesHandler(IMemoryCacheService memoryCachedRecipe)
    {
        _memoryCachedRecipe = memoryCachedRecipe ?? throw new ArgumentNullException(nameof(memoryCachedRecipe));
        //_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<Recipe>> Handle(GetRecipesQuery query, CancellationToken cancellationToken)
    {
        var isValidEnum = Enum.TryParse(
            query.MainIngredient,
            true,
            out IngredientType parsedIngredient)
            && Enum.IsDefined(typeof(IngredientType), parsedIngredient);

           if(isValidEnum is false) return new List<Recipe>();


        var collectedQuery = string.Join(",", query.MainIngredient.ToLower(), query.MealType.ToLower());

        var content = await _memoryCachedRecipe.GetCachedRecipes(parsedIngredient, collectedQuery);


        return content;
    }
}
