using Application.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSubstitute;
using RecipeApi.Application.Common.Settings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RecipeApi.Domain.Entities;
using RecipeApi.Infrastructure.Services;
using Xunit;

namespace Application.UnitTests.Services;

public class JwtServiceTests
{
    [Fact]
    public async Task JwtServiceShouldNotThrowException()
    {
        var appSettings = Substitute.For<IOptions<AppSettings>>();
        appSettings.Value.Returns(SettingsHelper.GetAppSettings());
        var user = UserObjectBuilder.GetDefaultUser();

        var sut = new JwtService(appSettings);

        var exception = await Record.ExceptionAsync(() => sut.GenerateJwtToken(user, CancellationToken.None));

        Assert.Null(exception);

    }

    [Fact]
    public async Task JwtServiceShouldGenerateNewToken()
    {
        var appSettings = Substitute.For<IOptions<AppSettings>>();
        appSettings.Value.Returns(SettingsHelper.GetAppSettings());
        var user = UserObjectBuilder.GetDefaultUser();

        var sut = new JwtService(appSettings);

        var token = await sut.GenerateJwtToken(user, CancellationToken.None);

        token.Should().NotBe(null);
    }

    [Fact]
    public async Task JwtServiceWithInvalidTokenShouldReturnNull()
    {
        var appSettings = Substitute.For<IOptions<AppSettings>>();
        appSettings.Value.Returns(SettingsHelper.GetAppSettings());

        var sut = new JwtService(appSettings);

        var token = await sut.ValidateJwtToken(null, CancellationToken.None);

        token.Should().Be(null);
    }

    [Fact]
    public async Task JwtServiceShouldValidateToken()
    {
        var appSettings = Substitute.For<IOptions<AppSettings>>();
        appSettings.Value.Returns(SettingsHelper.GetAppSettings());
        var value = appSettings.Value;

        var sut = new JwtService(appSettings);
        var token = GetNewToken(value.Secret);

        var result = await sut.ValidateJwtToken(token, CancellationToken.None);

        result.Should().NotBe(null);
    }

    [Fact]
    public async Task JwtServiceShouldReturnNewRefreshToken()
    {
        var appSettings = Substitute.For<IOptions<AppSettings>>();
        appSettings.Value.Returns(SettingsHelper.GetAppSettings());
        var sut = new JwtService(appSettings);
        
        var result = await sut.GenerateRefreshToken("192.168.0.1", CancellationToken.None);

        result.Should().NotBe(null);
        result.Should().BeOfType<RefreshToken>();
    }

    private static string GetNewToken(string secret)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = "";

        var key = Encoding.ASCII.GetBytes(secret);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", "1") }),
            Expires = DateTime.Now.AddMinutes(15),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var newToken = tokenHandler.CreateToken(tokenDescriptor);
        token = tokenHandler.WriteToken(newToken);

        return token;
    }

}
