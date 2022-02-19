using System.Collections.Generic;
using System.Linq;
using Application.UnitTests.Helpers;
using FluentAssertions;
using RecipeApi.Application.Recipes.Queries.GetRecipeCollections;
using System.Threading;
using System.Threading.Tasks;
using RecipeApi.Application.Common.Models;
using Xunit;

namespace Application.UnitTests.Handlers;

public class GetRecipeCollectionDetailsHandlerTests
{
    [Fact]
    public async Task HandlerShouldNotThrowException()
    {
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var mapper = AutoMapperHelper.GetAutoMapper();
        var handler = new GetRecipeCollectionDetailsHandler(applicationDbContext, mapper);
        var request = new GetRecipeDayQuery(1, 1);

        var exception = await Record.ExceptionAsync(() => handler.Handle(request, CancellationToken.None));

        Assert.Null(exception);
    }

    [Fact]
    public async Task HandlerShouldReturnEmptyListOfRecipeDays()
    {
        var mapper = AutoMapperHelper.GetAutoMapper();
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var handler = new GetRecipeCollectionDetailsHandler(applicationDbContext, mapper);
        var request = new GetRecipeDayQuery(1, 99999999);

        var result = await handler.Handle(request, CancellationToken.None);


        result.Should().BeOfType<List<RecipeDayResponse>>();
        result.Should().HaveCount(0);
    }

    [Fact]
    public async Task HandlerShouldReturnListOfRecipeDaysWithOneItem()
    {
        var user = UserObjectBuilder.GetDefaultUser();
        var mapper = AutoMapperHelper.GetAutoMapper();
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var handler = new GetRecipeCollectionDetailsHandler(applicationDbContext, mapper);
        var request = new GetRecipeDayQuery(1, 1);
        
        applicationDbContext.Add(user);
        await applicationDbContext.SaveChangesAsync();
        var result = await handler.Handle(request, CancellationToken.None);


        result.Should().BeOfType<List<RecipeDayResponse>>();
        result.Should().HaveCountGreaterOrEqualTo(1);
    }

}
