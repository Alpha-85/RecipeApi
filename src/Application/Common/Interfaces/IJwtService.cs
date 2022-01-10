
using RecipeApi.Domain.Entities;

namespace RecipeApi.Application.Common.Interfaces;

public interface IJwtService
{
    Task<string> GenerateJwtToken(User user,CancellationToken cancellationToken);
    Task<int?> ValidateJwtToken(string token, CancellationToken cancellationToken);
    Task<RefreshToken> GenerateRefreshToken(string ipAddress, CancellationToken cancellationToken);
}
