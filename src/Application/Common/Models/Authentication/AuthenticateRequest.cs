using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace RecipeApi.Application.Common.Models.Authentication;
[ExcludeFromCodeCoverage]
public class AuthenticateRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
