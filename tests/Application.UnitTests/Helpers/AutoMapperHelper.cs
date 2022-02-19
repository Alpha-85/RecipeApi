using AutoMapper;
using RecipeApi.Application.Common.Mappings;

namespace Application.UnitTests.Helpers;

public static class AutoMapperHelper
{
    public static IMapper GetAutoMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        var mapper = config.CreateMapper();

        return mapper;
    }
}
