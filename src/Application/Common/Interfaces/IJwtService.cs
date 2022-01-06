
using RecipeApi.Domain.Entities;

namespace RecipeApi.Application.Common.Interfaces;

public interface IJwtService
{
    public string GenerateJwtToken(User user);
    public int? ValidateJwtToken(string token);
    public RefreshToken GenerateRefreshToken(string ipAddress);
}
