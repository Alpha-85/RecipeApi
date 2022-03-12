using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using RecipeApi.Application.Common.Models;
using RecipeApi.Application.Common.Models.Recipes.RecipeResponse;
using RecipeApi.Application.Recipes.Commands.AddRecipeCollection;
using RecipeApi.Application.Recipes.Commands.DeleteRecipeCollection;
using RecipeApi.Application.Recipes.Queries.GetRecipeIngredients;
using RecipeApi.WebAppApi.Controllers;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Controllers;

public class RecipeCollectionControllerTests
{
    [Fact]
    public async Task ControllerShouldNotThrowException()
    {
        var mediator = Substitute.For<IMediator>();


        var sut = new RecipeCollectionController(mediator);

        var exception = await Record.ExceptionAsync(() => sut.GetAsync(1));

        Assert.Null(exception);

    }

    [Fact]
    public async Task ControllerGetAsyncShouldReturnAStatusCode200()
    {
        var mediator = Substitute.For<IMediator>();

        var sut = new RecipeCollectionController(mediator);

        var result = await sut.GetAsync(1);

        Assert.IsType<OkObjectResult>(result);

    }

    [Fact]
    public async Task ControllerPostAsyncShouldReturnAStatusCode200()
    {
        var mediator = Substitute.For<IMediator>();

        var sut = new RecipeCollectionController(mediator);
        mediator.Send(Arg.Any<AddRecipeCollectionCommand>()).Returns(new RecipeCollectionResponse());

        var result = await sut.PostAsync(1, "Alfons");

        Assert.IsType<OkObjectResult>(result);

    }

    [Fact]
    public async Task ControllerPostAsyncShouldReturnAStatusCode400()
    {
        var mediator = Substitute.For<IMediator>();

        var sut = new RecipeCollectionController(mediator);
        mediator.Send(Arg.Any<AddRecipeCollectionCommand>()).Returns((RecipeCollectionResponse)null!);

        var result = await sut.PostAsync(9999, "NoneValidCollection");

        Assert.IsType<BadRequestObjectResult>(result);

    }

    [Fact]
    public async Task ControllerDeleteAsyncShouldReturnAStatusCode200()
    {
        var mediator = Substitute.For<IMediator>();

        var sut = new RecipeCollectionController(mediator);
        mediator.Send(Arg.Any<DeleteRecipeCollectionCommand>()).Returns(true);

        var result = await sut.DeleteAsync(1, 1);

        Assert.IsType<OkObjectResult>(result);

    }

    [Fact]
    public async Task ControllerDeleteAsyncShouldReturnAStatusCode404()
    {
        var mediator = Substitute.For<IMediator>();

        var sut = new RecipeCollectionController(mediator);
        mediator.Send(Arg.Any<DeleteRecipeCollectionCommand>()).Returns(false);

        var result = await sut.DeleteAsync(1, 1);

        Assert.IsType<NotFoundResult>(result);

    }

    [Fact]
    public async Task ControllerGetShoppingListAsyncShouldReturnAStatusCode200()
    {
        var mediator = Substitute.For<IMediator>();

        var sut = new RecipeCollectionController(mediator);
        mediator.Send(Arg.Any<GetShoppingIngredientsQuery>()).Returns(new RecipeShoppingListResponse());

        var result = await sut.GetShoppingListAsync(1, 1);

        Assert.IsType<OkObjectResult>(result);

    }
}
