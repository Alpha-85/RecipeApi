using AutoMapper;
using MediatR;
using RecipeApi.Application.Common.Interfaces;
using RecipeApi.Application.Common.Models;

namespace RecipeApi.Application.Recipes.Queries.GetRecipe;

public class GetRecipeHandler : IRequestHandler<GetRecipeQuery, RecipeViewModel>
{
    private readonly ISpoonAdapter _spoonAdapter;
    private readonly IMapper _mapper;
    public GetRecipeHandler(ISpoonAdapter spoonAdapter, IMapper mapper)
    {
        _spoonAdapter = spoonAdapter ?? throw new ArgumentNullException(nameof(spoonAdapter));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public async Task<RecipeViewModel> Handle(GetRecipeQuery request, CancellationToken cancellationToken)
    {
        var recipe = await _spoonAdapter.GetRecipeAsync(request.RecipeId);
        var recipeViewModel = _mapper.Map<RecipeViewModel>(recipe);

        return recipeViewModel;
    }
}

