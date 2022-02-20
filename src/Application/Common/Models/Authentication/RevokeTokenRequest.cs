
using System.Diagnostics.CodeAnalysis;

namespace RecipeApi.Application.Common.Models.Authentication;

[ExcludeFromCodeCoverage]
public class RevokeTokenRequest
{
    public string Token { get; set; }
}
