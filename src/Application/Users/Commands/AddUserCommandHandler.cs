using MediatR;
using Microsoft.Extensions.Logging;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Domain.Entities;
using BCryptNet = BCrypt.Net.BCrypt;

namespace RecipeApi.Application.Users.Commands;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<AddUserCommandHandler> _logger;

    public AddUserCommandHandler(IApplicationDbContext context, ILogger<AddUserCommandHandler> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
       
    }
    public async Task<bool> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var userExists = _context.Users.FirstOrDefault(u => u.UserName == request.Username);

        if (userExists is not null)
        {
            _logger.LogError("User already exists",request.Username);
            return false;
        }

        var hashedPassword = BCryptNet.HashPassword(request.Password);
       
        await _context.Users.AddAsync(new User()
        { UserName = request.Username, PasswordHash = hashedPassword }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
