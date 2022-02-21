using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using RecipeApi.Application.Adapters;
using RecipeApi.Application.Common.Settings;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Adapters;

public class SpoonAdapterTests
{
    [Fact]
    public async Task SpoonAdapterShouldNotThrowException()
    {
        var appSettings = Substitute.For<IOptions<SpoonApiSettings>>();
        appSettings.Value.Returns(GetApiSettings());
        var logger = Substitute.For<ILogger<SpoonAdapter>>();
        var httpClient = Substitute.For<HttpClient>();

        var sut = new SpoonAdapter(httpClient, logger, appSettings);

        var exception = await Record.ExceptionAsync(() => sut.GetRecipeIngredientsAsync(1));

        Assert.Null(exception);

    }

    private static SpoonApiSettings GetApiSettings()
    {
        var settings = new SpoonApiSettings
        {
            ApiKey = "1b2ddd254"
        };
        return settings;
    }
}
