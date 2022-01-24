
using AutoMapper;
using MediatR;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models;
using RecipeApi.Domain.Entities;

namespace RecipeApi.Application.Recipes.Commands.AddRecipeCollection;

public class AddRecipeCollectionCommandHandler : IRequestHandler<AddRecipeCollectionCommand, RecipeCollectionViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
  
    public AddRecipeCollectionCommandHandler(IApplicationDbContext context,IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public async Task<RecipeCollectionViewModel> Handle(AddRecipeCollectionCommand request, CancellationToken cancellationToken)
    {
        var collection = new RecipeCollection()
        {
            UserId = request.UserId,
            CollectionName = request.CollectionName,
        };

        await _context.RecipeCollections.AddAsync(collection, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<RecipeCollectionViewModel>(collection);

        return result;
    }
}
