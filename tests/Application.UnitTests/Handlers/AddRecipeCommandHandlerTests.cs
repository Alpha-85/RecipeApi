using Application.UnitTests.Helpers;
using RecipeApi.Application.Recipes.Commands;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Handlers;

public class AddRecipeCommandHandlerTests
{
    [Fact]
    public async Task HandlerShouldNotThrowException()
    {
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var mapper = AutoMapperHelper.GetAutoMapper();
        var recipe = RequestObjectBuilder.GetUserRecipeRequest();
        var handler = new AddRecipeCommandHandler(applicationDbContext, mapper);
        var request = new AddRecipeCommand(recipe);

        var exception = await Record.ExceptionAsync(() => handler.Handle(request, CancellationToken.None));

        Assert.Null(exception);
    }

    [Fact]
    public async Task HandlerShouldReturnTrueWithNewRecipe()
    {
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var mapper = AutoMapperHelper.GetAutoMapper();
        var recipe = RequestObjectBuilder.GetUserRecipeRequest();
        var handler = new AddRecipeCommandHandler(applicationDbContext, mapper);
        var request = new AddRecipeCommand(recipe);

        var result = await handler.Handle(request, CancellationToken.None);
        
        Assert.True(result);
    }

    [Fact]
    public async Task HandlerShouldReturnFalseIfRecipeDayAlreadyExist()
    {
        var mapper = AutoMapperHelper.GetAutoMapper();
        var recipe = RequestObjectBuilder.GetUserRecipeRequest();
        var user = UserObjectBuilder.GetDefaultUser();
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        applicationDbContext.Add(user);
        await applicationDbContext.SaveChangesAsync();
        var handler = new AddRecipeCommandHandler(applicationDbContext, mapper);
        var request = new AddRecipeCommand(recipe);

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result);
    }
}
