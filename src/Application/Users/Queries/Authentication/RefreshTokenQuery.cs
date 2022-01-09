using MediatR;
using RecipeApi.Application.Common.Models.Authentication;

namespace RecipeApi.Application.Users.Queries.Authentication;

public class RefreshTokenQuery : IRequest<AuthenticateResponse>
{
    public string RefreshToken { get; }
    public string IpAddress { get; }

    public RefreshTokenQuery(in string refreshToken, in string ipAddress)
    {
        RefreshToken = refreshToken;
        IpAddress = ipAddress;
    }
}
