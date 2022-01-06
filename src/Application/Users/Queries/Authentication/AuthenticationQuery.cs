
using MediatR;
using RecipeApi.Application.Common.Models.Authentication;

namespace RecipeApi.Application.Users.Queries.Authentication;

public class AuthenticationQuery : IRequest<AuthenticateResponse>
{
    public string Username { get;  }
    public string Password { get;  }
    public string IpAddress { get; }

    public AuthenticationQuery(in string username,in string password,in string ipAddress)
    {
        Username = username;
        Password = password;
        IpAddress = ipAddress;
    }

}
