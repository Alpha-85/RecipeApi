
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using NSubstitute;
using NSubstitute.Core;
using RecipeApi.Application.Authorization;
using RecipeApi.Application.Common.Settings;
using RecipeApi.Domain.Entities;
using Xunit;

namespace Application.UnitTests.Services;

public class JwtServiceTests
{
    [Fact]
    public async Task JwtServiceShouldNotThrowException()
    {
        var appSettings = Substitute.For<IOptions<AppSettings>>();
        appSettings.Value.Returns(new AppSettings
        {
            Secret = "d3fl423d3fl423d3fl423d3fl423d3fl423d3fl423d3fl423d3fl423d3fl423d3fl423d3fl423d3fl423",
            RefreshTokenTTL = 2
        });
        
        var sut = new JwtService(appSettings);

        var exception = await Record.ExceptionAsync(() => sut.GenerateJwtToken(new User
        {
            Created = DateTime.MinValue,
            LastModified = DateTime.MinValue,
            Id = 1,
            UserName = "ttt",
            PasswordHash = "3fs34v",
            RefreshTokens = null,
            RecipeCollections = null
        }, CancellationToken.None));



        Assert.Null(exception);

    }

}
