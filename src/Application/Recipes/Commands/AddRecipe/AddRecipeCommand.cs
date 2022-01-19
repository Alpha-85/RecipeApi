﻿
using MediatR;
using RecipeApi.Application.Common.Models.UserRecipes;

namespace RecipeApi.Application.Recipes.Commands;

public class AddRecipeCommand : IRequest<bool>
{
    public UserRecipeRequest UserRecipe { get; }

    public AddRecipeCommand(UserRecipeRequest userRecipe)
    {
        UserRecipe = userRecipe;
    }
}
