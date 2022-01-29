using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models;

namespace RecipeApi.Application.Recipes.Queries.GetRecipeCollections;

public class GetRecipeCollectionHandler : IRequestHandler<GetRecipeCollectionQuery, List<RecipeCollectionResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetRecipeCollectionHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<RecipeCollectionResponse>> Handle(GetRecipeCollectionQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.RecipeCollections
            .Where(x => x.UserId == request.Id)
            .AsQueryable();

        return await queryable.Select(x => _mapper.Map<RecipeCollectionResponse>(x))
            .ToListAsync(cancellationToken);
    }
}
