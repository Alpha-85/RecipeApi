using RecipeApi.Application.Common.Models;
using RecipeApi.Application.Common.Models.Recipes;
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

    public static RecipeRequest GetRecipeRequest()
    {
        var request = new RecipeRequest
        {
            MealType = MealType.Dinner,
            Preference = PreferenceType.Beef,
            Allergies = new AllergiesViewModel
            {
                IsMilk = false,
                IsGluten = false,
                IsNuts = false,
                IsEgg = false,
                IsShellfish = false
            },
        };

        return request;
    }
    public static RecipeRequest GetDessertRecipeRequest()
    {
        var request = new RecipeRequest
        {
            MealType = MealType.Dessert,
            Preference = PreferenceType.Dessert,
            Allergies = new AllergiesViewModel
            {
                IsMilk = true,
                IsGluten = false,
                IsNuts = false,
                IsEgg = true,
                IsShellfish = false
            },
        };

        return request;
    }
}
