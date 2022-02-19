
using Application.UnitTests.Helpers;
using FluentAssertions;
using RecipeApi.Application.Common.Models;
using RecipeApi.Application.Recipes.Queries.GetRecipeCollections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Handlers;

public class GetRecipeCollectionHandlerTests
{
    [Fact]
    public async Task HandlerShouldNotThrowException()
    {
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var mapper = AutoMapperHelper.GetAutoMapper();
        var handler = new GetRecipeCollectionHandler(applicationDbContext, mapper);
        var request = new GetRecipeCollectionQuery(1);

        var exception = await Record.ExceptionAsync(() => handler.Handle(request, CancellationToken.None));

        Assert.Null(exception);
    }

    [Fact]
    public async Task HandlerShouldReturnEmptyCollection()
    {
        var mapper = AutoMapperHelper.GetAutoMapper();
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var handler = new GetRecipeCollectionHandler(applicationDbContext, mapper);
        var request = new GetRecipeCollectionQuery(999);

        var result = await handler.Handle(request, CancellationToken.None);


        result.Should().BeOfType<List<RecipeCollectionResponse>>();
        result.Should().HaveCount(0);
    }

    [Fact]
    public async Task HandlerShouldReturnListOfRecipeDaysWithOneItem()
    {
        var user = UserObjectBuilder.GetDefaultUser();
        var mapper = AutoMapperHelper.GetAutoMapper();
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var handler = new GetRecipeCollectionHandler(applicationDbContext, mapper);
        var request = new GetRecipeCollectionQuery(1);

        applicationDbContext.Add(user);
        await applicationDbContext.SaveChangesAsync();
        var result = await handler.Handle(request, CancellationToken.None);


        result.Should().BeOfType<List<RecipeCollectionResponse>>();
        result.Should().HaveCountGreaterOrEqualTo(1);
    }
}
