using RecipeApi.Domain.Entities;
using System;
using System.Security.Cryptography;

namespace Application.UnitTests.Helpers;

public static class AuthenticateHelper
{
    public static RefreshToken GetRefreshToken()
    {
        byte[] bytes = new byte[64];
        var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);

        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(bytes),
            Expires = DateTime.MaxValue,
            Created = DateTime.MinValue,
            CreatedByIp = "192.168.0.133"
        };

        return refreshToken;
    }

    public static RefreshToken GetBadRefreshToken()
    {
        byte[] bytes = new byte[64];
        var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);

        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(bytes),
            Expires = DateTime.MinValue,
            Created = DateTime.MinValue,
            CreatedByIp = "192.168.0.133"
        };

        return refreshToken;
    }

}
