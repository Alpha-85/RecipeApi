using Application.UnitTests.Helpers;
using FluentAssertions;
using NSubstitute;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models;
using RecipeApi.Application.Recipes.Queries.GetRecipe;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Handlers;
public class GetRecipeHandlerTests
{
    [Fact]
    public async Task GetRecipeShouldNotThrowException()
    {
        var mapper = AutoMapperHelper.GetAutoMapper();
        var spoonAdapter = Substitute.For<ISpoonAdapter>();
        var handler = new GetRecipeHandler(spoonAdapter, mapper);
        var request = new GetRecipeQuery(1);

        var exception = await Record.ExceptionAsync(() => handler.Handle(request, CancellationToken.None));

        Assert.Null(exception);
    }

    [Fact]
    public async Task GetRecipeShouldReturnRecipeViewModelRecipe()
    {
        var mapper = AutoMapperHelper.GetAutoMapper();
        var spoonAdapter = Substitute.For<ISpoonAdapter>();
        var handler = new GetRecipeHandler(spoonAdapter, mapper);
        var request = new GetRecipeQuery(1);
        var recipe = RecipeObjectBuilder.GetSingleRecipe();
        spoonAdapter.GetRecipeAsync(1).Returns(recipe);

        var result = await handler.Handle(request, CancellationToken.None);

        result.Should().BeOfType<RecipeViewModel>();
        result.Should().NotBeNull();
    }
}
