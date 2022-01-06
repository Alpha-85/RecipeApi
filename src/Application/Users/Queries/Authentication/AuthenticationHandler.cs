
using MediatR;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models.Authentication;

namespace RecipeApi.Application.Users.Queries.Authentication;

public class AuthenticationHandler: IRequestHandler<AuthenticationQuery, AuthenticateResponse>
{
    private readonly IUserService _userService;

    public AuthenticationHandler(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    public async Task<AuthenticateResponse> Handle(AuthenticationQuery query, CancellationToken cancellationToken)
    {
        var request = new AuthenticateRequest
        {
            Username = query.Username,
            Password = query.Password,
            
        };

        var response = await _userService.Authenticate(request,query.IpAddress, cancellationToken);

        if (response == null)
            return null;

        return response;
    }
}
