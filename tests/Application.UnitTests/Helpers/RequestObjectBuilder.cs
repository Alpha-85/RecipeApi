using System.Collections.Generic;
using RecipeApi.Application.Common.Models;
using RecipeApi.Application.Common.Models.SpoonResponse;
using RecipeApi.Application.Common.Models.UserRecipes;
using RecipeApi.Domain.Enums;

namespace Application.UnitTests.Helpers;

public static class RequestObjectBuilder
{
    public static UserRecipeRequest GetUserRecipeRequest()
    {
        var request = new UserRecipeRequest
        {
            UserId = 1,
            CollectionId = 1,
            WeekdayId = 1,
            RecipeInformation = new RecipeInformationViewModel
            {
                RecipeName = "Meatballs",
                RecipeId = 123456,
                ReadyInMinutes = 30,
                RecipeUrl = "www.meatballs.com",
                Image = "www.meatballs.com/meatball.jpg",
                MealType = MealType.Breakfast
            }

        };

        return request;
    }

    public static List<Recipe> GetRecipes()
    {
        var recipes = new List<Recipe>();

        recipes.Add(new Recipe
        {
            Vegetarian = false,
            Vegan = false,
            GlutenFree = false,
            DairyFree = false,
            VeryHealthy = false,
            Cheap = false,
            VeryPopular = false,
            Sustainable = false,
            WeightWatcherSmartPoints = 0,
            Gaps = "",
            LowFodmap = false,
            AggregateLikes = 0,
            SpoonacularScore = 0,
            HealthScore = 0,
            CreditsText = "",
            License = "",
            SourceName = "",
            PricePerServing = 0,
            ExtendedIngredients = new List<ExtendedIngredient>
            {
                Capacity = 1
            },
            Id = 0,
            Title = "",
            ReadyInMinutes = 0,
            Servings = 0,
            SourceUrl = "",
            Image = "",
            ImageType = "",
            Summary = "",
            Cuisines = new List<string>(){""},
            DishTypes = new List<string>(){""},
            Diets = new List<string>(){""},
            Occasions = new List<object>(),
            Instructions = "",
            AnalyzedInstructions = new List<AnalyzedInstruction>
            {
                Capacity = 1
            },
            OriginalId = new object(),
            SpoonacularSourceUrl = ""
        });

        return recipes;
    }
}
