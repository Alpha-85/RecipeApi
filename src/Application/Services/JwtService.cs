using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Settings;
using RecipeApi.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RecipeApi.Application.Authorization;


public class JwtService : IJwtService
{
    private readonly AppSettings _appSettings;

    public JwtService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public async Task<string> GenerateJwtToken(User user,CancellationToken cancellationToken)
    {
        // gör en ny token som håller i 15 minuter
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = "";

        await Task.Run(() =>
        {
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var newToken = tokenHandler.CreateToken(tokenDescriptor);
            token = tokenHandler.WriteToken(newToken);
        }, cancellationToken);
       
        return token;
    }

    public async Task<int?> ValidateJwtToken(string token,CancellationToken cancellationToken)
    {
        if (token is null)
            return null;

        int? userId = null;
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

        await Task.Run(() =>
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

        },cancellationToken);

        return userId;

    }

    public async Task<RefreshToken> GenerateRefreshToken(string ipAddress,CancellationToken cancellationToken)
    {
        var refreshToken = new RefreshToken();

        await Task.Run(() =>
        {
            byte[] bytes = new byte[64];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);

            refreshToken.Token = Convert.ToBase64String(bytes);
            refreshToken.Expires = DateTime.UtcNow.AddDays(7);
            refreshToken.Created = DateTime.UtcNow;
            refreshToken.CreatedByIp = ipAddress;
           

        },cancellationToken);

        if(!Task.CompletedTask.IsCompleted)
        return null;
        
        return refreshToken;
    }
}
