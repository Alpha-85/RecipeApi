using System.ComponentModel.DataAnnotations;


namespace RecipeApi.Application.Common.Models.Authentication;

public class AuthenticateRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
