using Application.UnitTests.Helpers;
using RecipeApi.Application.Recipes.Commands.AddRecipeCollection;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using RecipeApi.Application.Common.Models;
using Xunit;

namespace Application.UnitTests.Handlers;

public class AddRecipeCollectionCommandHandlerTests
{
    [Fact]
    public async Task HandlerShouldNotThrowException()
    {
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var mapper = AutoMapperHelper.GetAutoMapper();
        var handler = new AddRecipeCollectionCommandHandler(applicationDbContext, mapper);
        var request = new AddRecipeCollectionCommand("AlfonsFavvisar", 1);

        var exception = await Record.ExceptionAsync(() => handler.Handle(request, CancellationToken.None));

        Assert.Null(exception);
    }

    [Fact]
    public async Task HandlerShouldAddNewCollection()
    {
        var user = UserObjectBuilder.GetDefaultUser();
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var mapper = AutoMapperHelper.GetAutoMapper();
        var handler = new AddRecipeCollectionCommandHandler(applicationDbContext, mapper);
        var request = new AddRecipeCollectionCommand("DessertWeek", 1);

        applicationDbContext.Add(user);
        await applicationDbContext.SaveChangesAsync();
        var result = await handler.Handle(request, CancellationToken.None);

        result.Should().BeOfType<RecipeCollectionResponse>();
        Assert.NotNull(result);
    }

    [Fact]
    public async Task HandlerShouldReturnNullIfCollectionAlreadyExists()
    {
        var user = UserObjectBuilder.GetDefaultUser();
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var mapper = AutoMapperHelper.GetAutoMapper();
        var handler = new AddRecipeCollectionCommandHandler(applicationDbContext, mapper);
        var request = new AddRecipeCollectionCommand("AlfonsFavvisar", 1);

        applicationDbContext.Add(user);
        await applicationDbContext.SaveChangesAsync();
        var result = await handler.Handle(request, CancellationToken.None);


        result.Should().Be(null);
    }
}
