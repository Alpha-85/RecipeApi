using Application.IntegrationTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using RecipeApi.Application.Users.Commands;
using System.Threading;
using System.Threading.Tasks;
using Xunit;



namespace Application.IntegrationTests.Handlers;

public class AddUserCommandHandlerTests
{
    [Fact]
    public async Task AddedUserHandlerShouldNotThrowException()
    {
        var handler = CreateSut();
        var request = new AddUserCommand("Alfons", "1234");

        var exception = await Record.ExceptionAsync(() => handler.Handle(request, CancellationToken.None));
        Assert.Null(exception);
    }

    [Fact]
    public async Task HandlerShouldReturnCorrectUser()
    {
        var handler = CreateSut();
        var user = new AddUserCommand("Alfons", "1234");

        var result = await handler.Handle(user, CancellationToken.None);

        result.Should().BeTrue();
    }

    private static AddUserCommandHandler CreateSut()
    {
        var applicationContext = DbContextHelper.GetApplicationDbContext();
        var logger = NSubstitute.Substitute.For<ILogger<AddUserCommandHandler>>();
        var handler = new AddUserCommandHandler(applicationContext, logger);
        return handler;
    }
}
