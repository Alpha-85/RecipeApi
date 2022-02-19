using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;
using System.Linq;
using WebAppApi;
using Xunit;

namespace Application.IntegrationTests;

public class SwaggerTests
{
    [Fact]
    public void TestSwagger()
    {
        var server = Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(options => { options.UseStartup<Startup>(); })
            .Build();

        var swagger = server.Services
            .GetRequiredService<ISwaggerProvider>()
            .GetSwagger("v1");

        swagger.Should().NotBeNull();
        swagger.Paths.Any().Should().BeTrue();
        swagger.Components.Schemas.Should().NotBeNull();

    }

}
