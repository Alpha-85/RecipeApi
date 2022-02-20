using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using RecipeApi.Application.Users.Queries.Authentication;
using RecipeApi.WebAppApi.Controllers;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Controllers;

public class UserControllerTests
{
    [Fact(Skip = "Make IpAddress to Interface")]
    public async Task AuthenticateShouldReturnBadRequestIfUserIsNotFound()
    {
        var mediator = Substitute.For<IMediator>();
        var controller = new UsersController(mediator);


        await controller.AuthenticateUser("nobody", "444");
        var response = mediator.Send(new AuthenticationQuery("", "", "")).ReturnsNull();

        response.Should().BeNull();
        response.Should().BeAssignableTo<BadRequestResult>();

    }

}
