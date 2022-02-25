using System;
using Application.UnitTests.Helpers;
using NSubstitute;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Users.Queries.Authentication;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using RecipeApi.Application.Common.Models.Authentication;
using Xunit;

namespace Application.UnitTests.Handlers;

public class RefreshTokenHandlerTests
{
    [Fact]
    public async Task RefreshTokenHandlerShouldNotThrowException()
    {
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var jwtService = Substitute.For<IJwtService>();
        var userService = Substitute.For<IUserService>();
        var handler = new RefreshTokenHandler(applicationDbContext, jwtService, userService);
        var refreshTokenQuery = new RefreshTokenQuery("token", "192.168.0.1");

        var exception = await Record.ExceptionAsync(() => handler.Handle(refreshTokenQuery, CancellationToken.None));

        Assert.Null(exception);
    }

    [Fact]
    public async Task RefreshTokenShouldReturnNull()
    {
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var jwtService = Substitute.For<IJwtService>();
        var userService = Substitute.For<IUserService>();
        var user = UserObjectBuilder.GetUserForAuth();
        var handler = new RefreshTokenHandler(applicationDbContext, jwtService, userService);
        applicationDbContext.Add(user);
        await applicationDbContext.SaveChangesAsync();

        var request = new RefreshTokenQuery("dummy", "192.168.0.1");

        var result = await handler.Handle(request, CancellationToken.None);

        result.Should().Be(null);

    }

    [Fact]
    public async Task RefreshTokenShouldReturnNullIfNotActive()
    {
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var jwtService = Substitute.For<IJwtService>();
        var userService = Substitute.For<IUserService>();
        var user = UserObjectBuilder.GetUserForAuth();
        var refreshToken = AuthenticateHelper.GetBadRefreshToken();
        var handler = new RefreshTokenHandler(applicationDbContext, jwtService, userService);
        user.RefreshTokens.Add(refreshToken);
        applicationDbContext.Add(user);
        await applicationDbContext.SaveChangesAsync();

        var request = new RefreshTokenQuery(refreshToken.Token, "192.168.0.1");

        var result = await handler.Handle(request, CancellationToken.None);

        result.Should().Be(null);
    }

    [Fact]
    public async Task RefreshTokenShouldReturnNewAuthenticateResponseWithRotatedToken()
    {
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var jwtService = Substitute.For<IJwtService>();
        var userService = Substitute.For<IUserService>();
        var user = UserObjectBuilder.GetUserForAuth();
        var refreshToken = AuthenticateHelper.GetRefreshToken();
        var handler = new RefreshTokenHandler(applicationDbContext, jwtService, userService);
        user.RefreshTokens.Add(refreshToken);
        applicationDbContext.Add(user);
        await applicationDbContext.SaveChangesAsync();
        userService.RotateRefreshToken(refreshToken,"192.168.0.1",CancellationToken.None).Returns(AuthenticateHelper.GetRefreshToken());

        var request = new RefreshTokenQuery(refreshToken.Token, "192.168.0.1");

        var result = await handler.Handle(request, CancellationToken.None);

        result.Should().BeOfType<AuthenticateResponse>();
        result.Should().NotBe(null);
        result.RefreshToken.Should().NotBeSameAs(refreshToken.Token);
    }

    [Fact]
    public async Task RevokedRefreshTokenShouldResultInNull()
    {
        var applicationDbContext = DbContextHelper.GetApplicationDbContext();
        var jwtService = Substitute.For<IJwtService>();
        var userService = Substitute.For<IUserService>();
        var user = UserObjectBuilder.GetUserForAuth();
        var refreshToken = AuthenticateHelper.GetBadRefreshToken();
        refreshToken.Revoked = DateTime.Now;
        var handler = new RefreshTokenHandler(applicationDbContext, jwtService, userService);
        user.RefreshTokens.Add(refreshToken);
        applicationDbContext.Add(user);
        await applicationDbContext.SaveChangesAsync();

        var request = new RefreshTokenQuery(refreshToken.Token, "192.168.0.1");

        var result = await handler.Handle(request, CancellationToken.None);

        result.Should().Be(null);
    }


}
