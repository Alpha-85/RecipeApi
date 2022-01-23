using AutoMapper;
using MediatR;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models;
using RecipeApi.Application.Common.Models.Recipes;
using RecipeApi.Application.Common.Models.SpoonResponse;
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

    public async Task<List<RecipeViewModel>> Handle(GetRecipesQuery recipe, CancellationToken cancellationToken)
    {
        var value = EnumChecker(recipe.Request.MealType);
        var result = new List<RecipeViewModel>();

        if (value is 1 && recipe.Request.Preference
            is PreferenceType.Breakfast
            or PreferenceType.Dessert
          )
        {
            var cachedData = await _memoryCachedRecipe
                .GetCachedRecipes(recipe.Request.Preference, recipe.Request.Preference.ToString().ToLower());

            var content = GetThreeRandomRecipes(cachedData, recipe.Request.Allergies);
            result = content.Select(recipe => _mapper.Map<RecipeViewModel>(recipe)).ToList();

        }
        if (value is 2)
        {
            var collectedQuery = string
                .Join(",", recipe.Request.Preference.ToString().ToLower()
                , recipe.Request.MealType.ToString().ToLower());

            var cachedData = await _memoryCachedRecipe
            .GetCachedRecipes(recipe.Request.Preference, recipe.Request.Preference.ToString().ToLower());

            var content = GetThreeRandomRecipes(cachedData, recipe.Request.Allergies);
            result = content.Select(recipe => _mapper.Map<RecipeViewModel>(recipe)).ToList();

        }

        return result;
    }

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

    private List<Recipe> GetThreeRandomRecipes(List<Recipe> listToFilter, Allergies allergies)
    {
        Random random = new();
        if (String.IsNullOrWhiteSpace(allergies.OtherAllergies)
            && allergies.IsDairyFree is false
            && allergies.IsGlutenFree is false)
            return listToFilter.OrderBy(x => random.Next()).Take(3).ToList();

        if (!String.IsNullOrWhiteSpace(allergies.OtherAllergies))
            listToFilter = OtherAllergies(listToFilter, allergies);


        return listToFilter
            .OrderBy(x => random.Next())
            .Take(3)
            .Where(r => r.DairyFree == allergies.IsDairyFree 
             && r.GlutenFree == allergies.IsGlutenFree)
            .ToList();
    }

    private static List<Recipe> OtherAllergies(List<Recipe> listToFilter, Allergies allergies)
    {
        /// Seafood,Egg,Nuts etc Milk, Eggs, Other Dairy
        if (allergies.OtherAllergies.Contains(','))
        {
            var values = allergies.OtherAllergies.Split(',');

            return listToFilter
                .Where(r => r.ExtendedIngredients
                .Any(s => !values.Contains(s.Aisle)))
                .ToList();
        }
        // Or singleValue
        return listToFilter
          .Where(r => r.ExtendedIngredients
          .Any(s => !s.Aisle.Contains(allergies.OtherAllergies)))
          .ToList();
    }
    ///// Seafood,Egg,Nuts etc
    //if (allergies.OtherAllergies.Contains(','))
    //{
    //    var values = allergies.OtherAllergies.Split(',');

    //    return listToFilter
    //        .OrderBy(x => random.Next())
    //        .Take(3)
    //        .Where(r => r.ExtendedIngredients
    //        .Any(s => !values.Contains(s.Aisle)))
    //        .ToList();
    //}
    //else
    //{
    //    return listToFilter
    //        .OrderBy(x => random.Next())
    //        .Take(3)
    //        .Where(r => r.ExtendedIngredients
    //        .Any(s => !s.Aisle.Contains(allergies.OtherAllergies)))
    //        .ToList();
    //}
}
