using AutoMapper;
using RecipeApi.Application.Common.Models;
using RecipeApi.Application.Common.Models.SpoonResponse;

namespace RecipeApi.Application.Common.Mappings;

public class MappingProfile : Profile
{

    public MappingProfile()
    {
        CreateMap<Recipe, RecipeViewModel>();
           
    }
}
