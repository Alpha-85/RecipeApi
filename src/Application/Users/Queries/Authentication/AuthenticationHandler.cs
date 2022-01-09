
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models.Authentication;
using RecipeApi.Application.Common.Settings;
using RecipeApi.Domain.Entities;
using BCryptNet = BCrypt.Net.BCrypt;

namespace RecipeApi.Application.Users.Queries.Authentication;

public class AuthenticationHandler : IRequestHandler<AuthenticationQuery, AuthenticateResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly AppSettings _appSettings;

    public AuthenticationHandler(IApplicationDbContext context, IJwtService jwtService, IOptions<AppSettings> appSettings)
    {
        _context = context ?? throw new ArgumentNullException(nameof(_context));
        _jwtService = jwtService ?? throw new ArgumentNullException(nameof(_jwtService));
        _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(_appSettings));
    }

    public async Task<AuthenticateResponse> Handle(AuthenticationQuery query, CancellationToken cancellationToken)
    {
        var user = await _context.Users.Include(r => r.RefreshTokens).FirstOrDefaultAsync(x => x.UserName == query.Username, cancellationToken);

        if (user == null || !BCryptNet.Verify(query.Password, user.PasswordHash))
            return null;

        var jwtToken = await _jwtService.GenerateJwtToken(user, cancellationToken);

        var refreshToken = await _jwtService.GenerateRefreshToken(query.IpAddress, cancellationToken);

        user.RefreshTokens.Add(refreshToken);

        RemoveOldRefreshTokens(user);

        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);

        return new AuthenticateResponse(user, jwtToken, refreshToken.Token);

    }

    private void RemoveOldRefreshTokens(User user)
    {
        user.RefreshTokens.RemoveAll(x =>
            !x.IsActive &&
            x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
    }
}
