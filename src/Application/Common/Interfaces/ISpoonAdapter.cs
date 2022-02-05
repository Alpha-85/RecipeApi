﻿using RecipeApi.Application.Common.Models.SpoonResponse;

namespace RecipeApi.Application.Common.Interfaces;

public interface ISpoonAdapter
{
    Task<List<Recipe>> GetRandomRecipesAsync(string query);
    Task<List<ExtendedIngredient>> GetRecipeIngredientsAsync(int recipeId);
}
