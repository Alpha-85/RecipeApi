using RecipeApi.Application.Common.Models.Authentication;
using RecipeApi.Domain.Entities;

namespace RecipeApi.Application.Common.Interfaces;

public interface IUserService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress, CancellationToken cancellationToken);
    Task<AuthenticateResponse> RefreshToken(string token, string ipAddress, CancellationToken cancellationToken);
    Task<bool> RevokeToken(string token, string ipAddress, CancellationToken cancellationToken);
    Task<string> HashUserPassword(string password, CancellationToken cancellationToken);
}
