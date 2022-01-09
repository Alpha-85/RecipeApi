using RecipeApi.Application.Common.Models.Authentication;
using RecipeApi.Domain.Entities;

namespace RecipeApi.Application.Common.Interfaces;

public interface IUserService
{
    Task<AuthenticateResponse> RefreshToken(string token, string ipAddress, CancellationToken cancellationToken);
    Task<bool> RevokeToken(string token, string ipAddress, CancellationToken cancellationToken);
    public User GetById(int id);
}
