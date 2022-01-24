using MediatR;

namespace RecipeApi.Application.Users.Commands;

public class AddUserCommand : IRequest<bool>
{
    public string Username { get; }
    public string Password { get; }

    public AddUserCommand(in string username, in string password)
    {
        Username = username;
        Password = password;
    }
}
