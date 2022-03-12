using System.Collections.Generic;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using RecipeApi.Application.Common.Models.Recipes;
using RecipeApi.WebAppApi.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeApi.Application.Common.Models;
using RecipeApi.Application.Common.Models.UserRecipes;
using RecipeApi.Application.Recipes.Commands;
using RecipeApi.Application.Recipes.Queries.GetRandomRecipes;
using Xunit;

namespace Application.UnitTests.Controllers;

public class RecipeControllerTests
{
    [Fact]
    public async Task ControllerShouldNotThrowException()
    {
        var mediator = Substitute.For<IMediator>();
        var logger = Substitute.For<ILogger<RecipeController>>();

        var sut = new RecipeController(mediator, logger);

        var exception = await Record.ExceptionAsync(() => sut.Get(new RecipeRequest()));

        Assert.Null(exception);

    }

    [Fact]
    public async Task ControllerGetAsyncShouldReturnAStatusCode200()
    {
        var mediator = Substitute.For<IMediator>();
        var logger = Substitute.For<ILogger<RecipeController>>();
        var sut = new RecipeController(mediator,logger);
        mediator.Send(Arg.Any<GetRecipesQuery>()).Returns(new List<RecipeViewModel>());


        var result = await sut.Get(new RecipeRequest());

        Assert.IsType<OkObjectResult>(result);

    }

    [Fact]
    public async Task ControllerPostAsyncShouldReturnAStatusCode400()
    {
        var mediator = Substitute.For<IMediator>();
        var logger = Substitute.For<ILogger<RecipeController>>();
        var sut = new RecipeController(mediator,logger);
        mediator.Send(Arg.Any<AddRecipeCommand>()).Returns(false);


        var result = await sut.PostAsync(new UserRecipeRequest());

        Assert.IsType<BadRequestObjectResult>(result);

    }

    [Fact]
    public async Task ControllerPostAsyncShouldReturnAStatusCode200()
    {
        var mediator = Substitute.For<IMediator>();
        var logger = Substitute.For<ILogger<RecipeController>>();
        var sut = new RecipeController(mediator, logger);
        mediator.Send(Arg.Any<AddRecipeCommand>()).Returns(true);


        var result = await sut.PostAsync(new UserRecipeRequest());

        Assert.IsType<OkObjectResult>(result);

    }




}
