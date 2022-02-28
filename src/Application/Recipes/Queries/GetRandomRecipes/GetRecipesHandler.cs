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
        var allergies = _mapper.Map<Allergies>(recipe.Request.Allergies);

        var result = new List<RecipeViewModel>();

        // Switch case depending on what kind of InMemoryList it should save or retrieve from.
        switch (value)
        {
            case 1 when recipe.Request.Preference
                is PreferenceType.Breakfast
                or PreferenceType.Dessert:
                {
                    var cachedData = await _memoryCachedRecipe
                        .GetCachedRecipes(recipe.Request.Preference,
                            recipe.Request.Preference.ToString()
                                .ToLower());

                    var content = GetThreeRandomRecipes(cachedData,
                        allergies);
                    result = content.Select(source => _mapper.Map<RecipeViewModel>(source))
                        .ToList();
                    break;
                }
            case 2:
                {
                    var cachedData = await _memoryCachedRecipe
                        .GetCachedRecipes(recipe.Request.Preference,
                            recipe.Request.Preference.ToString()
                                .ToLower());

                    var content = GetThreeRandomRecipes(cachedData,
                        allergies);
                    result = content.Select(source => _mapper.Map<RecipeViewModel>(source))
                        .ToList();
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

    private static IEnumerable<Recipe> GetThreeRandomRecipes(List<Recipe> listToFilter, Allergies allergies)
    {
        Random random = new();
        var filtered = new List<Recipe>();

        if (!string.IsNullOrEmpty(allergies.OtherAllergies))
        {
            filtered = FilterByOtherAllergies(listToFilter, allergies);
            var recipes = filtered.OrderBy(x => random.Next())
                .Where(r => r.DairyFree == allergies.IsDairyFree
                            && r.GlutenFree == allergies.IsGlutenFree)
                .Take(3)
                .ToList();

            return recipes;

        }

        filtered = listToFilter
           .OrderBy(x => random.Next())
           .Where(r => r.DairyFree == allergies.IsDairyFree
                       && r.GlutenFree == allergies.IsGlutenFree)
           .Take(3)
           .ToList();

        return filtered;
    }

    private static List<Recipe> FilterByOtherAllergies(List<Recipe> listToFilter, Allergies allergies)
    {
        var filtered = new List<Recipe>();

        if (allergies.OtherAllergies.Contains(','))
        {
            var values = allergies.OtherAllergies.Split(',');

             filtered = listToFilter
                .Where(r => r.ExtendedIngredients
                    .All(i => !values
                        .Any(s => i.Name
                            .Contains(s))))
                .ToList();

            return filtered;

        }

        filtered =  listToFilter
            .Where(r => r.ExtendedIngredients
                .All(s => !s.Name.Contains(allergies.OtherAllergies)))
            .ToList();

        return filtered;
    }

}
