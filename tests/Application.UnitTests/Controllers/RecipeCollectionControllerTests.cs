using MediatR;
using NSubstitute;
using RecipeApi.WebAppApi.Controllers;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Controllers;

public class RecipeCollectionControllerTests
{
    [Fact]
    public async Task SpoonAdapterShouldNotThrowException()
    {
        var mediator = Substitute.For<IMediator>();


        var sut = new RecipeCollectionController(mediator);

        var exception = await Record.ExceptionAsync(() => sut.GetAsync(1));

        Assert.Null(exception);

    }
}
