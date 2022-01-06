

using RecipeApi.Domain.Entities;
using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models.Authentication;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string JwtToken { get; set; }

    [JsonIgnore] // refresh token is returned in http only cookie
    public string RefreshToken { get; set; }

    public AuthenticateResponse(User user, string jwtToken, string refreshToken)
    {
        Id = user.Id;
        Username = user.UserName;
        JwtToken = jwtToken;
        RefreshToken = refreshToken;
    }
}
