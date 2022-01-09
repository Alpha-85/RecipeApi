using Microsoft.Extensions.Options;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Settings;
using RecipeApi.Domain.Entities;

namespace RecipeApi.Application.Services;

public class UserService : IUserService
{
    private readonly IJwtService _jwtService;
    private readonly AppSettings _appSettings;


    public UserService(
        IJwtService jwtService,
        IOptions<AppSettings> appSettings)
    {
        _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
    }

    public async Task<RefreshToken> RotateRefreshToken(RefreshToken refreshToken, string ipAddress, CancellationToken cancellationToken)
    {
        var newRefreshToken = await _jwtService.GenerateRefreshToken(ipAddress, cancellationToken);
        RevokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
        return newRefreshToken;
    }

    public Task RemoveOldRefreshTokens(User user)
    {
        user.RefreshTokens.RemoveAll(x =>
            !x.IsActive &&
            x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);

        return Task.CompletedTask;
    }

    public Task RevokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason)
    {
        if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
        {
            var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
            if (childToken.IsActive)
                RevokeRefreshToken(childToken, ipAddress, reason);
            else
                RevokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
        }
        return Task.CompletedTask;
    }

    private void RevokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
    {
        token.Revoked = DateTime.UtcNow;
        token.RevokedByIp = ipAddress;
        token.ReasonRevoked = reason;
        token.ReplacedByToken = replacedByToken;
    }
}
