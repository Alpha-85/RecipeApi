using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using RecipeApi.Application.Common.Models.Recipes;
using RecipeApi.WebAppApi.Controllers;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Controllers;

public class RecipeControllerTests
{
    [Fact]
    public async Task SpoonAdapterShouldNotThrowException()
    {
        var mediator = Substitute.For<IMediator>();
        var logger = Substitute.For<ILogger<RecipeController>>();

        var sut = new RecipeController(mediator, logger);

        var exception = await Record.ExceptionAsync(() => sut.Get(new RecipeRequest()));

        Assert.Null(exception);

    }
}
