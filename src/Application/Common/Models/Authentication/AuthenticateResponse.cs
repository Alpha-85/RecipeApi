

using RecipeApi.Domain.Entities;

namespace RecipeApi.Application.Common.Models.Authentication;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string JwtToken { get; set; }
    public string RefreshToken { get; set; }

    public AuthenticateResponse(User user, string jwtToken, string refreshToken)
    {
        Id = user.Id;
        Username = user.UserName;
        JwtToken = jwtToken;
        RefreshToken = refreshToken;
    }
}
