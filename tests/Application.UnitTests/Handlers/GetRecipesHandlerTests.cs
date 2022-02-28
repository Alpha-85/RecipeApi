
using Application.UnitTests.Helpers;
using FluentAssertions;
using NSubstitute;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models;
using RecipeApi.Application.Common.Models.SpoonResponse;
using RecipeApi.Application.Recipes.Queries.GetRandomRecipes;
using RecipeApi.Domain.Enums;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Handlers;

public class GetRecipesHandlerTests
{
    [Fact]
    public async Task GetRecipesShouldNotThrowException()
    {
        var mapper = AutoMapperHelper.GetAutoMapper();
        var memoryService = Substitute.For<IMemoryCacheService>();
        var handler = new GetRecipesHandler(memoryService, mapper);
        var recipe = new GetRecipesQuery(RequestObjectBuilder.GetRecipeRequest());
        memoryService.GetCachedRecipes(PreferenceType.Beef, "beef").Returns(RecipeObjectBuilder.GetListWithThreeRecipes());

        var exception = await Record.ExceptionAsync(() => handler.Handle(recipe, CancellationToken.None));

        Assert.Null(exception);
    }

    [Fact]
    public async Task GetRecipesShouldReturnRecipeViewModelWithThreeRandomRecipes()
    {
        var mapper = AutoMapperHelper.GetAutoMapper();
        var memoryService = Substitute.For<IMemoryCacheService>();
        var handler = new GetRecipesHandler(memoryService, mapper);
        var recipe = new GetRecipesQuery(RequestObjectBuilder.GetRecipeRequest());
        memoryService.GetCachedRecipes(PreferenceType.Beef, "beef").Returns(RecipeObjectBuilder.GetListWithThreeRecipes());

        var result = await handler.Handle(recipe, CancellationToken.None);

        result.Should().BeOfType<List<RecipeViewModel>>();
        result.Should().HaveCount(3);
    }

    [Fact]
    public async Task GetRecipesWithAllergiesShouldReturnListOfThreeRecipes()
    {
        var mapper = AutoMapperHelper.GetAutoMapper();
        var memoryService = Substitute.For<IMemoryCacheService>();
        var handler = new GetRecipesHandler(memoryService, mapper);
        var recipe = new GetRecipesQuery(RequestObjectBuilder.GetDessertRecipeRequest());
        memoryService.GetCachedRecipes(PreferenceType.Dessert, "dessert").Returns(RecipeObjectBuilder.GetListWithSixRecipes());

        var result = await handler.Handle(recipe, CancellationToken.None);

        result.Should().BeOfType<List<RecipeViewModel>>();
        result.Should().HaveCount(3);
    }

    [Fact]
    public async Task GetRecipesShouldReturnEmptyList()
    {
        var mapper = AutoMapperHelper.GetAutoMapper();
        var memoryService = Substitute.For<IMemoryCacheService>();
        var handler = new GetRecipesHandler(memoryService, mapper);
        var recipe = new GetRecipesQuery(RequestObjectBuilder.GetRecipeRequest());
        memoryService.GetCachedRecipes(PreferenceType.Beef, "beef").Returns(new List<Recipe>());

        var result = await handler.Handle(recipe, CancellationToken.None);

        result.Should().BeOfType<List<RecipeViewModel>>();
        result.Should().BeEmpty();
    }
}

