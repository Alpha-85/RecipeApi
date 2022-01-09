using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models.Authentication;
using RecipeApi.Application.Common.Settings;
using RecipeApi.Domain.Entities;

namespace RecipeApi.Application.Users.Queries.Authentication;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenQuery, AuthenticateResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly AppSettings _appSettings;
    private readonly IUserService _userService;

    public RefreshTokenHandler(IApplicationDbContext context, IJwtService jwtService, IOptions<AppSettings> appSettings,IUserService userService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(_context));
        _jwtService = jwtService ?? throw new ArgumentNullException(nameof(_jwtService));
        _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(_appSettings));
        _userService = userService ?? throw new ArgumentNullException(nameof(_userService));
    }
    public async Task<AuthenticateResponse> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var user = GetUserByRefreshToken(request.RefreshToken);
        if (user is null)
            return null;

        var refreshToken = user.RefreshTokens.Single(x => x.Token == request.RefreshToken);

        if (refreshToken.IsRevoked)
        {
            await _userService.RevokeDescendantRefreshTokens(refreshToken, user, request.IpAddress,
                $"Attempted reuse of revoked ancestor token: {request.RefreshToken}");

            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        if (!refreshToken.IsActive)
            return null;


        // replace old refresh token with a new one (rotate token)
        var newRefreshToken = await _userService.RotateRefreshToken(refreshToken, request.IpAddress, cancellationToken);
        user.RefreshTokens.Add(newRefreshToken);

        // remove old refresh tokens from user
        await _userService.RemoveOldRefreshTokens(user);

        // save changes to db
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);

        // generate new jwt
        var jwtToken = await _jwtService.GenerateJwtToken(user, cancellationToken);

        return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
    }

    private User GetUserByRefreshToken(string token)
    {
        var user = _context.Users.Include(r => r.RefreshTokens)
            .Where(u => u.RefreshTokens.Any(t => t.Token == token))
            .FirstOrDefault();

        if(user is null)
            return null;

        return user;
    }
}
