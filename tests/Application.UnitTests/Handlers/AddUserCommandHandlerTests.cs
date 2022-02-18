using Application.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using RecipeApi.Application.Users.Commands;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;



namespace Application.UnitTests.Handlers;

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
    public async Task HandlerShouldAddNewUserAndReturnTrue()
    {
        var handler = CreateSut();
        var user = new AddUserCommand("Alfons", "1234");

        var result = await handler.Handle(user, CancellationToken.None);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task HandlerShouldNotAddUserAndReturnFalse()
    {
        var existingUser = UserObjectBuilder.GetDefaultUser();
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        applicationDbContext.Add(existingUser);
        await applicationDbContext.SaveChangesAsync();

        var logger = Substitute.For<ILogger<AddUserCommandHandler>>();
        var handler = new AddUserCommandHandler(applicationDbContext, logger);


        var newUser = new AddUserCommand("Alfons", "1234");

        var result = await handler.Handle(newUser, CancellationToken.None);

        result.Should().BeFalse();
    }

    private static AddUserCommandHandler CreateSut()
    {
        var applicationContext = DbContextHelper.GetApplicationDbContext();
        var logger = Substitute.For<ILogger<AddUserCommandHandler>>();
        var handler = new AddUserCommandHandler(applicationContext, logger);
        return handler;
    }
}

