using MediatR;
using RecipeApi.Application.Common.Models;
using RecipeApi.Domain.Enums;

namespace RecipeApi.Application.Recipes.Queries.GetRandomRecipies;

public class GetRecipesQuery : IRequest<List<RecipeViewModel>>
{
    public MealType MealType { get; }
    public PreferenceType Preference { get; }
    public string Allergies { get; }

    public GetRecipesQuery(in MealType mealType, in PreferenceType preference, in string allergies)
    {
        MealType = mealType;
        Preference = preference;
        Allergies = allergies;
    }
}
