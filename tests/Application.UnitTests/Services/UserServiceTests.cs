
using Application.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Settings;
using RecipeApi.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using RecipeApi.Infrastructure.Services;
using Xunit;

namespace Application.UnitTests.Services;

public class UserServiceTests
{
    [Fact]
    public async Task UserServiceShouldNotThrowException()
    {
        var appSettings = Substitute.For<IOptions<AppSettings>>();
        var jwtService = Substitute.For<IJwtService>();
        appSettings.Value.Returns(SettingsHelper.GetAppSettings());
        var user = UserObjectBuilder.GetDefaultUser();

        var sut = new UserService(jwtService, appSettings);

        var exception = await Record.ExceptionAsync(() => sut.RemoveOldRefreshTokens(user));

        Assert.Null(exception);

    }

    [Fact]
    public void UserServiceShouldRemoveOldRefreshTokens()
    {
        var appSettings = Substitute.For<IOptions<AppSettings>>();
        var jwtService = Substitute.For<IJwtService>();
        appSettings.Value.Returns(SettingsHelper.GetAppSettings());
        var user = UserObjectBuilder.GetDefaultUser();

        var sut = new UserService(jwtService, appSettings);

        var result = sut.RemoveOldRefreshTokens(user);

        result.Should().Be(Task.CompletedTask);
    }

    [Fact]
    public void UserServiceShouldRevokeDescendantRefreshTokens()
    {
        var appSettings = Substitute.For<IOptions<AppSettings>>();
        var jwtService = Substitute.For<IJwtService>();
        appSettings.Value.Returns(SettingsHelper.GetAppSettings());
        var user = UserObjectBuilder.GetDefaultUser();

        var sut = new UserService(jwtService, appSettings);

        var result = sut.RevokeDescendantRefreshTokens(new RefreshToken(), user, "192.168.0.1", "Revoked");

        result.Should().Be(Task.CompletedTask);
    }

    [Fact]
    public async Task UserServiceShouldRotateRefreshToken()
    {
        var appSettings = Substitute.For<IOptions<AppSettings>>();
        var jwtService = Substitute.For<IJwtService>();
        appSettings.Value.Returns(SettingsHelper.GetAppSettings());
        var value = appSettings.Value;
        var refreshToken = AuthenticateHelper.GetRefreshToken();

        jwtService.GenerateRefreshToken("192.168.0.1", CancellationToken.None).Returns(AuthenticateHelper.GetRefreshToken());

        var sut = new UserService(jwtService, appSettings);

        var result = await sut.RotateRefreshToken(refreshToken, "192.168.0.1", CancellationToken.None);

        result.Should().BeOfType<RefreshToken>();
        result.RevokedByIp.Should().NotBeSameAs(refreshToken.RevokedByIp);
    }

}
