﻿using MediatR;
using RecipeApi.Application.Common.Models;
using RecipeApi.Application.Common.Models.SpoonResponse;

namespace RecipeApi.Application.Recipes.Queries.GetRandomRecipies;

public class GetRecipesQuery : IRequest<List<RecipeViewModel>>
{
    public string MealType { get; }
    public string MainIngredient { get; }

    public GetRecipesQuery(in string mealType,in string mainIngredient)
    {
        MealType = mealType;
        MainIngredient = mainIngredient;
    }
}
