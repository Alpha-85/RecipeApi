using Application.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models.Authentication;
using RecipeApi.Application.Common.Settings;
using RecipeApi.Application.Users.Queries.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Application.UnitTests.Handlers;

public class AuthenticationHandlerTests
{
    [Fact]
    public async Task HandlerShouldNotThrowException()
    {
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var jwtService = Substitute.For<IJwtService>();
        var appSettings = Substitute.For<IOptions<AppSettings>>();
        appSettings.Value.Returns(SettingsHelper.GetAppSettings());
        var handler = new AuthenticationHandler(applicationDbContext, jwtService, appSettings);
        var request = new AuthenticationQuery("Alfons", "14333344", "192.168.0.1");

        var exception = await Record.ExceptionAsync(() => handler.Handle(request, CancellationToken.None));

        Assert.Null(exception);
    }

    [Fact]
    public async Task HandlerShouldReturnNull()
    {
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var jwtService = Substitute.For<IJwtService>();
        var appSettings = Substitute.For<IOptions<AppSettings>>();
        appSettings.Value.Returns(SettingsHelper.GetAppSettings());
        var handler = new AuthenticationHandler(applicationDbContext, jwtService, appSettings);
        var request = new AuthenticationQuery("Alfons", "14333344", "192.168.0.1");

        var result = await handler.Handle(request, CancellationToken.None);

        result.Should().Be(null);
    }

    [Fact]
    public async Task HandlerShouldReturnAuthenticateResponse()
    {
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var user = UserObjectBuilder.GetUserForAuth();
        user.PasswordHash = BCryptNet.HashPassword("14333344");
        applicationDbContext.Users.Add(user);
        await applicationDbContext.SaveChangesAsync();
        var jwtService = Substitute.For<IJwtService>();
        var appSettings = Substitute.For<IOptions<AppSettings>>();
        appSettings.Value.Returns(SettingsHelper.GetAppSettings());
        var handler = new AuthenticationHandler(applicationDbContext, jwtService, appSettings);
        var request = new AuthenticationQuery("Alfons", "14333344", "192.168.0.1");
        jwtService.GenerateJwtToken(user, CancellationToken.None).Returns("");
        jwtService.GenerateRefreshToken(Arg.Any<string>(), CancellationToken.None).Returns(AuthenticateHelper.GetRefreshToken());


        var result = await handler.Handle(request, CancellationToken.None);


        result.Should().BeOfType<AuthenticateResponse>();
        result.Should().NotBe(null);
    }
}
