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
    private readonly IRecipeFilteredService _recipeFilterService;

    public GetRecipesHandler(IMemoryCacheService memoryCachedRecipe, IMapper mapper, IRecipeFilteredService recipeFilter)
    {
        _memoryCachedRecipe = memoryCachedRecipe ?? throw new ArgumentNullException(nameof(memoryCachedRecipe));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _recipeFilterService = recipeFilter ?? throw new ArgumentNullException(nameof(recipeFilter));
    }

    public async Task<List<RecipeViewModel>> Handle(GetRecipesQuery query, CancellationToken cancellationToken)
    {
        var value = EnumChecker(query.MealType);
        var result = new List<RecipeViewModel>();

        // breakfast eller dessert
        if (value is 1 && query.Preference 
            is  PreferenceType.Breakfast 
            or PreferenceType.Dessert 
          )
        {
            //result.AddRange(await _memoryCachedRecipe
            //    .GetCachedRecipes(query.Preference, query.Preference
            //    .ToString()
            //    .ToLower()));
        }
        if (value is 2)
        {
            var collectedQuery = string
                .Join(",", query.Preference.ToString().ToLower()
                , query.MealType.ToString().ToLower());
            //result.AddRange(await _memoryCachedRecipe
            //    .GetCachedRecipes(query.Preference, collectedQuery));
          
        }


        return result;

        //var isValidEnum = Enum.TryParse(
        //    query.MainIngredient,
        //    true,
        //    out IngredientType parsedIngredient)
        //    && Enum.IsDefined(typeof(IngredientType), parsedIngredient);

        //if (isValidEnum is false) return new List<RecipeViewModel>();


        //var collectedQuery = string.Join(",", query.MainIngredient, query.MealType);

        //var content = await _memoryCachedRecipe.GetCachedRecipes(parsedIngredient, collectedQuery);

        //var result = new List<RecipeViewModel>();

        //result = content.Select(recipe => _mapper.Map<RecipeViewModel>(recipe)).ToList(); 

        //return result;
    }
    // om allergy strängen är tom lägg inte till i strin.joing 
    // 

    private int EnumChecker(MealType mealType)
    {
        switch (mealType)
        {
            case MealType.Breakfast:
                return 1;
            case MealType.Dessert:
                return 1;
            case MealType.Dinner:
                return 2;
            case MealType.Lunch:
                return 2;
            case MealType.FineDinner:
                return 2;

            default: return 0;
        }
    }
}
