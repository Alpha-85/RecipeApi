using Application.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models.SpoonResponse;
using RecipeApi.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApi.Infrastructure.Services;
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
        var recipes = RecipeObjectBuilder.GetListWithSingleRecipe();
        const string query = "beef";
        var category = PreferenceType.Beef;
        var sut = new MemoryCacheService(memory, spoonAdapter);
        memory.Get(category).Returns(true, recipes);
        spoonAdapter.GetRandomRecipesAsync(query).Returns(recipes);


        var result = await sut.GetCachedRecipes(category, query);

        result.Should().NotBeNull();
        result.Should().BeOfType<List<Recipe>>();

    }

    [Fact]
    public async Task ExistingIMemoryCacheListShouldBeRetrieved()
    {
        var spoonAdapter = Substitute.For<ISpoonAdapter>();
        var memory = GetMemoryCache();
        var sut = new MemoryCacheService(memory, spoonAdapter);
        var recipes = RecipeObjectBuilder.GetListWithSingleRecipe();
        const string query = "beef";
        const PreferenceType category = PreferenceType.Beef;

        var result = await sut.GetCachedRecipes(category, query);

        result.Should().BeOfType<List<Recipe>>();
        result.Count.Should().Be(1);
        result.Should().BeEquivalentTo(recipes);
    }


    private static IMemoryCache GetMemoryCache()
    {
        var cache = new MemoryCache(new MemoryCacheOptions());
        var recipes = RecipeObjectBuilder.GetListWithSingleRecipe();

        cache.Set(PreferenceType.Beef, recipes);

        return cache;
    }

}
