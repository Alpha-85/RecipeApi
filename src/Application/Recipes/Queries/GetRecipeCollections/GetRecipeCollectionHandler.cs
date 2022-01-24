using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models;

namespace RecipeApi.Application.Recipes.Queries.GetRecipeCollections;

public class GetRecipeCollectionHandler : IRequestHandler<GetRecipeCollectionQuery, List<RecipeCollectionViewModel>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetRecipeCollectionHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<RecipeCollectionViewModel>> Handle(GetRecipeCollectionQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.RecipeCollections.Where(c => c.UserId == request.Id)
            .AsQueryable();

        return await queryable.Select(x => _mapper.Map<RecipeCollectionViewModel>(x))
            .ToListAsync(cancellationToken);
    }
}
