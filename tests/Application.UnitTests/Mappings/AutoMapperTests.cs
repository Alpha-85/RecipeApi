using AutoMapper;
using RecipeApi.Application.Common.Mappings;
using Xunit;

namespace Application.UnitTests.Mappings;

public class AutoMapperTests
{
    [Fact]
    public void AllMappingShouldBeValid()
    {
        var configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile<MappingProfile>());

        configuration.AssertConfigurationIsValid();

    }

}
