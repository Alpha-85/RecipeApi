using Application.UnitTests.Helpers;
using FluentAssertions;
using RecipeApi.Application.Recipes.Commands.DeleteRecipeCollection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Handlers;

public class DeleteRecipeCollectionCommandHandlerTests
{
    [Fact]
    public async Task HandlerShouldNotThrowException()
    {
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var handler = new DeleteRecipeCollectionCommandHandler(applicationDbContext);
        var request = new DeleteRecipeCollectionCommand(1, 1);

        var exception = await Record.ExceptionAsync(() => handler.Handle(request, CancellationToken.None));

        Assert.Null(exception);
    }

    [Fact]
    public async Task HandlerShouldDeleteCollection()
    {
        var user = UserObjectBuilder.GetDefaultUser();
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var handler = new DeleteRecipeCollectionCommandHandler(applicationDbContext);
        var request = new DeleteRecipeCollectionCommand(1, 1);

        applicationDbContext.Add(user);
        await applicationDbContext.SaveChangesAsync();
        var result = await handler.Handle(request, CancellationToken.None);

        result.Should().Be(true);
    }

    [Fact]
    public async Task HandlerShouldNotDeleteCollectionIfCollectionIdDoesNotExist()
    {
        var user = UserObjectBuilder.GetDefaultUser();
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var handler = new DeleteRecipeCollectionCommandHandler(applicationDbContext);
        var request = new DeleteRecipeCollectionCommand(1, 75465465);

        applicationDbContext.Add(user);
        await applicationDbContext.SaveChangesAsync();
        var result = await handler.Handle(request, CancellationToken.None);

        result.Should().Be(false);
    }
}
