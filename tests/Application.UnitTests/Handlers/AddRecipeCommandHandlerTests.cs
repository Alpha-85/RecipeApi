using System.Threading;
using Application.UnitTests.Helpers;
using AutoMapper;
using NSubstitute;
using RecipeApi.Application.Common.Models.UserRecipes;
using RecipeApi.Application.Recipes.Commands;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Handlers;

public class AddRecipeCommandHandlerTests
{
    [Fact(Skip = "Not done")]
    public async Task HandlerShouldNotThrowException()
    {
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var mapper = Substitute.For<IMapper>();
        var recipe = RequestObjectBuilder.GetUserRecipeRequest();
        var handler = new AddRecipeCommandHandler(applicationDbContext, mapper);
        var request = new AddRecipeCommand(recipe);

        var exception = await Record.ExceptionAsync(() => handler.Handle(request, CancellationToken.None));

        Assert.Null(exception);
    }
}
