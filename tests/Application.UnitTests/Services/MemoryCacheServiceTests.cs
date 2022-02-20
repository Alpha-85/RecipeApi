
using System;
using Application.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models.SpoonResponse;
using RecipeApi.Application.Services;
using RecipeApi.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Services;

public class MemoryCacheServiceTests
{
    [Fact]
    public async Task MemoryCacheServiceShouldNotThrowException()
    {
        var memory = Substitute.For<IMemoryCache>();
        var spoonAdapter = Substitute.For<ISpoonAdapter>();
        const string query = "beef";

        var sut = new MemoryCacheService(memory, spoonAdapter);
        spoonAdapter.GetRandomRecipesAsync(query).Returns(new List<Recipe>());


        var exception = await Record.ExceptionAsync(() => sut.GetCachedRecipes(PreferenceType.Beef, query));

        Assert.Null(exception);
    }

    [Fact]
    public async Task GetCachedRecipesShouldNotReturnNull()
    {
        var memory = Substitute.For<IMemoryCache>();
        var spoonAdapter = Substitute.For<ISpoonAdapter>();
        var recipes = RequestObjectBuilder.GetRecipes();
        const string query = "beef";
        var category = PreferenceType.Beef;
        var sut = new MemoryCacheService(memory, spoonAdapter);
        memory.Get(category).Returns(true, recipes);
        spoonAdapter.GetRandomRecipesAsync(query).Returns(recipes);


        var result = await sut.GetCachedRecipes(category, query);

        result.Should().NotBeNull();
        result.Should().BeOfType<List<Recipe>>();

    }

}
