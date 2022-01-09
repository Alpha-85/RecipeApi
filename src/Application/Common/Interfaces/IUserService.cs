using RecipeApi.Application.Common.Models.Authentication;
using RecipeApi.Domain.Entities;

namespace RecipeApi.Application.Common.Interfaces;

public interface IUserService
{
    Task RevokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason);
    Task<RefreshToken> RotateRefreshToken(RefreshToken refreshToken, string ipAddress, CancellationToken cancellationToken);
    Task RemoveOldRefreshTokens(User user);
}
