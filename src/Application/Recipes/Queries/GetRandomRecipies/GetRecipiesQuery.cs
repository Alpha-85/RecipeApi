using MediatR;
using RecipeApi.Application.Common.Models;
using RecipeApi.Application.Common.Models.Recipes;
using RecipeApi.Domain.Enums;

namespace RecipeApi.Application.Recipes.Queries.GetRandomRecipies;

public class GetRecipesQuery : IRequest<List<RecipeViewModel>>
{
    public RecipeRequest Request { get; }

    public GetRecipesQuery(in RecipeRequest request)
    {
        Request = request;
    }
    //public MealType MealType { get; }
    //public PreferenceType Preference { get; }
    //public Allergies Allergies { get; }

    //public GetRecipesQuery(in MealType mealType, in PreferenceType preference, in Allergies allergies)
    //{
    //    MealType = mealType;
    //    Preference = preference;
    //    Allergies = allergies;
    //}
}
