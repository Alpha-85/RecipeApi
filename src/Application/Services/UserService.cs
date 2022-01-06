using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models.Authentication;
using RecipeApi.Application.Common.Settings;
using RecipeApi.Domain.Entities;
using BCryptNet = BCrypt.Net.BCrypt;

namespace RecipeApi.Application.Services;

public class UserService : IUserService
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly AppSettings _appSettings;


    public UserService(
        IApplicationDbContext context,
        IJwtService jwtService,
        IOptions<AppSettings> appSettings)
    {
        _context = context;
        _jwtService = jwtService;
        _appSettings = appSettings.Value;
    }

    public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress, CancellationToken cancellationToken)
    {
        var user = GetUserByRefreshToken(token);
        var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

        if (refreshToken.IsRevoked)
        {
            RevokeDescendantRefreshTokens(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        if (!refreshToken.IsActive)
            return null;
   

        // replace old refresh token with a new one (rotate token)
        var newRefreshToken = await RotateRefreshToken(refreshToken, ipAddress,cancellationToken);
        user.RefreshTokens.Add(newRefreshToken);

        // remove old refresh tokens from user
        RemoveOldRefreshTokens(user);

        // save changes to db
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);

        // generate new jwt
        var jwtToken = await _jwtService.GenerateJwtToken(user,cancellationToken);

        return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
    }

    public async Task<bool> RevokeToken(string token,string ipAddress, CancellationToken cancellationToken)
    {
        var user = GetUserByRefreshToken(token);
        var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

        if (!refreshToken.IsActive)
            return false;
        //    throw new AppException("Invalid token");

        // revoke token and save
        RevokeRefreshToken(refreshToken,ipAddress, "Revoked without replacement");
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    private User GetUserByRefreshToken(string token)
    {
        var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

        //if (user == null)
        //    throw new AppException("Invalid token");

        return user;
    }


    private async Task<RefreshToken> RotateRefreshToken(RefreshToken refreshToken, string ipAddress,CancellationToken cancellationToken)
    {
        var newRefreshToken = await _jwtService.GenerateRefreshToken(ipAddress,cancellationToken);
        RevokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
        return newRefreshToken;
    }

    private void RemoveOldRefreshTokens(User user)
    {
        user.RefreshTokens.RemoveAll(x =>
            !x.IsActive &&
            x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
    }

    private void RevokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason)
    {
        // recursively traverse the refresh token chain and ensure all descendants are revoked
        if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
        {
            var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
            if (childToken.IsActive)
                RevokeRefreshToken(childToken, ipAddress, reason);
            else
                RevokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
        }
    }

    private void RevokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
    {
        token.Revoked = DateTime.UtcNow;
        token.RevokedByIp = ipAddress;
        token.ReasonRevoked = reason;
        token.ReplacedByToken = replacedByToken;
    }
}
