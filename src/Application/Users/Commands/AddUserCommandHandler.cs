using MediatR;
using Microsoft.Extensions.Logging;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Domain.Entities;

namespace RecipeApi.Application.Users.Commands;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<AddUserCommandHandler> _logger;
    private readonly IUserService _userService;
    // private readonly IMapper mapper;

    public AddUserCommandHandler(IApplicationDbContext context, ILogger<AddUserCommandHandler> logger, IUserService userService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }
    public async Task<bool> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var hashed = await _userService.HashUserPassword(request.Password, cancellationToken);

        _context.Users.Add(new User()
        { UserName = request.Username, PasswordHash = hashed });

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
