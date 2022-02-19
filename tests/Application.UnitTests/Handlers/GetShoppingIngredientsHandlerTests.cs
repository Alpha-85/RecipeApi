using Application.UnitTests.Helpers;
using FluentAssertions;
using NSubstitute;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models.SpoonResponse;
using RecipeApi.Application.Recipes.Queries.GetRecipeIngredients;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Handlers;

public class GetShoppingIngredientsHandlerTests
{
    [Fact]
    public async Task HandlerShouldNotThrowException()
    {
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var mapper = AutoMapperHelper.GetAutoMapper();
        var spoonAdapter = Substitute.For<ISpoonAdapter>();
        var handler = new GetShoppingIngredientsHandler(applicationDbContext, mapper, spoonAdapter);
        var request = new GetShoppingIngredientsQuery(1, 1);

        var exception = await Record.ExceptionAsync(() => handler.Handle(request, CancellationToken.None));

        Assert.Null(exception);
    }

    [Fact]
    public async Task HandlerShouldReturnListOfIngredients()
    {
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var user = UserObjectBuilder.GetDefaultUser();
        var mapper = AutoMapperHelper.GetAutoMapper();
        var spoonAdapter = Substitute.For<ISpoonAdapter>();
        var handler = new GetShoppingIngredientsHandler(applicationDbContext, mapper, spoonAdapter);
        var request = new GetShoppingIngredientsQuery(1, 1);

        applicationDbContext.Add(user);
        await applicationDbContext.SaveChangesAsync();
        spoonAdapter.GetRecipeIngredientsAsync(635350)
            .Returns(SpoonApiData());


        var result = handler.Handle(request, CancellationToken.None);


        result.Should().NotBeNull();
    }

    private static List<ExtendedIngredient> SpoonApiData()
    {
        var data = new List<ExtendedIngredient>();

        data.Add(new ExtendedIngredient
        {
            Id = 1,
            Aisle = "e",
            Image = "www.img.com",
            Consistency = "tt",
            Name = "One",
            NameClean = "alfons",
            Original = "",
            OriginalString = "",
            OriginalName = "",
            Amount = 5,
            Unit = "",
            Meta = new List<string> { "1", "2" },
            MetaInformation = new List<string> { "1", "2" },
            Measures = new Measures
            {
                Us = new Us
                {
                    Amount = 5,
                    UnitShort = "",
                    UnitLong = ""
                },
                Metric = new Metric
                {
                    Amount = 5,
                    UnitShort = "",
                    UnitLong = ""
                }
            }
        });

        data.Add(new ExtendedIngredient
        {
            Id = 2,
            Aisle = "f",
            Image = "www.img2.com",
            Consistency = "tt2",
            Name = "Two",
            NameClean = "joanne",
            Original = "",
            OriginalString = "",
            OriginalName = "",
            Amount = 2,
            Unit = "",
            Meta = new List<string> { "1", "2" },
            MetaInformation = new List<string> { "1", "2" },
            Measures = new Measures
            {
                Us = new Us
                {
                    Amount = 5,
                    UnitShort = "",
                    UnitLong = ""
                },
                Metric = new Metric
                {
                    Amount = 5,
                    UnitShort = "",
                    UnitLong = ""
                }
            }
        });



        return data;
    }
}
