using MediatR;
using Microsoft.EntityFrameworkCore;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models.Authentication;
using RecipeApi.Domain.Entities;

namespace RecipeApi.Application.Users.Queries.Authentication;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenQuery, AuthenticateResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly IUserService _userService;

    public RefreshTokenHandler(IApplicationDbContext context, IJwtService jwtService, IUserService userService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
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

        var newRefreshToken = await _userService.RotateRefreshToken(refreshToken, request.IpAddress, cancellationToken);
        user.RefreshTokens.Add(newRefreshToken);

        await _userService.RemoveOldRefreshTokens(user);

        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);

        var jwtToken = await _jwtService.GenerateJwtToken(user, cancellationToken);

        return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
    }

    private User GetUserByRefreshToken(string token)
    {
        var user = _context.Users
            .Include(r => r.RefreshTokens)
            .FirstOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

        if (user is null)
            return null;

        return user;
    }
}
