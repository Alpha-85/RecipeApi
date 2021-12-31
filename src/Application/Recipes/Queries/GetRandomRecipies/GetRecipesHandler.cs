using MediatR;
using RecipeApi.Application.Common.Models.SpoonResponse;
using RecipeApi.Domain.Enums;

namespace RecipeApi.Application.Recipes.Queries.GetRandomRecipies;

public class GetRecipesHandler : IRequestHandler<GetRecipesQuery, List<Recipe>>
{
    private readonly IMapper _mapper;
    private readonly ISpoonAdapter _spoonAdapter;
    private readonly IMemoryCachedRecipe _memoryCachedRecipe;

    public GetRecipesHandler(IMapper mapper, ISpoonAdapter spoonAdapter, IMemoryCachedRecipe memoryCachedRecipe)
    {
        _memoryCachedRecipe = memoryCachedRecipe ?? throw new ArgumentNullException(nameof(memoryCachedRecipe));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _spoonAdapter = spoonAdapter ?? throw new ArgumentNullException(nameof(spoonAdapter));
    }

    public async Task<List<Recipe>> Handle(GetRecipesQuery query, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<IngredientType>(query.MainIngredient, ignoreCase: true, out var ingredientType))
            return null;

        var collectedQuery = string.Join(",", query.MainIngredient, query.MealType);

        var content = await _memoryCachedRecipe.GetCachedRecipes(ingredientType, collectedQuery);


        return content;
    }
}
