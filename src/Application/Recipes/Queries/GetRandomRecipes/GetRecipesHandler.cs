using AutoMapper;
using MediatR;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models;
using RecipeApi.Application.Common.Models.Recipes;
using RecipeApi.Application.Common.Models.SpoonResponse;
using RecipeApi.Domain.Enums;

namespace RecipeApi.Application.Recipes.Queries.GetRandomRecipes;

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

        // Switch case depending on what kind of InMemoryList it should save or get from.
        switch (value)
        {
            case 1 when recipe.Request.Preference
                is PreferenceType.Breakfast
                or PreferenceType.Dessert:
                {
                    var cachedData = await _memoryCachedRecipe
                        .GetCachedRecipes(recipe.Request.Preference, recipe.Request.Preference.ToString().ToLower());

                    var content = GetThreeRandomRecipes(cachedData, recipe.Request.Allergies);
                    result = content.Select(source => _mapper.Map<RecipeViewModel>(source)).ToList();
                    break;
                }
            case 2:
                {
                    var cachedData = await _memoryCachedRecipe
                        .GetCachedRecipes(recipe.Request.Preference, recipe.Request.Preference.ToString().ToLower());

                    var content = GetThreeRandomRecipes(cachedData, recipe.Request.Allergies);
                    result = content.Select(source => _mapper.Map<RecipeViewModel>(source)).ToList();
                    break;
                }
        }

        return result;
    }

    private static int EnumChecker(MealType mealType)
    {
        return mealType switch
        {
            MealType.Breakfast => 1,
            MealType.Dessert => 1,
            MealType.Dinner => 2,
            MealType.Lunch => 2,
            MealType.FineDinner => 2,
            _ => 0
        };
    }

    private static IEnumerable<Recipe> GetThreeRandomRecipes(IEnumerable<Recipe> listToFilter, Allergies allergies)
    {
        Random random = new();
        var filteredList = new List<Recipe>();

        if (string.IsNullOrEmpty(allergies.OtherAllergies)
            && allergies.IsDairyFree is false
            && allergies.IsGlutenFree is false)
        {
            filteredList = listToFilter.OrderBy(x => random.Next()).Take(3).ToList();
            return filteredList;
        }


        if (!string.IsNullOrEmpty(allergies.OtherAllergies))
        {
            filteredList = FilterByOtherAllergies(listToFilter, allergies);
        }

        return filteredList
            .OrderBy(x => random.Next())
            .Where(r => r.DairyFree == allergies.IsDairyFree
                        && r.GlutenFree == allergies.IsGlutenFree)
            .Take(3)
            .ToList();
    }

    private static List<Recipe> FilterByOtherAllergies(IEnumerable<Recipe> listToFilter, Allergies allergies)
    {
        // Seafood,Egg,Nuts , Milk, Eggs, Other Dairy,Ethnic Foods
        if (allergies.OtherAllergies.Contains(','))
        {
            var values = allergies.OtherAllergies.Split(',');

            var filtered = listToFilter
                .Where(r => r.ExtendedIngredients
                    .Any(i => !values
                        .Any(s => i.Name
                            .Contains(s))))
                .ToList();

            return filtered;

        }

        var filteredList = listToFilter
            .Where(r => r.ExtendedIngredients
                .Any(s => !s.Name.Contains(allergies.OtherAllergies)))
            .ToList();
        return filteredList;
    }

}
