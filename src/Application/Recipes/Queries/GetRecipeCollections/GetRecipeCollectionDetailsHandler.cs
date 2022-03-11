using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models;

namespace RecipeApi.Application.Recipes.Queries.GetRecipeCollections;

public class GetRecipeCollectionDetailsHandler : IRequestHandler<GetRecipeDayQuery, List<RecipeDayResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetRecipeCollectionDetailsHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<RecipeDayResponse>> Handle(GetRecipeDayQuery request, CancellationToken cancellationToken)
    {
        var details = await _context.RecipeCollections
            .Where(u => u.UserId == request.UserId && u.Id == request.CollectionId)
            .Include(r => r.RecipeDays)
            .ThenInclude(x => x.Recipes)
            .Include(x => x.RecipeDays)
            .ThenInclude(x => x.Weekday)
            .SelectMany(x => x.RecipeDays).OrderBy(x => x.WeekdayId)  
            .ToListAsync(cancellationToken);

        return details.Select(r => _mapper.Map<RecipeDayResponse>(r)).ToList();

    }
}
