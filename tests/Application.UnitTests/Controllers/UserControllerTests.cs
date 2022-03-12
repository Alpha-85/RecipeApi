using Application.UnitTests.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using RecipeApi.Application.Common.Models.Authentication;
using RecipeApi.Application.Users.Commands;
using RecipeApi.Application.Users.Queries.Authentication;
using RecipeApi.WebAppApi.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RecipeApi.Application.Common.Interfaces;
using Xunit;

namespace Application.UnitTests.Controllers;

public class UserControllerTests
{
    [Fact]
    public async Task ControllerShouldNotThrowException()
    {
        var mediator = Substitute.For<IMediator>();
        var extension = Substitute.For<IIpAddressExtensions>();

        var sut = new UsersController(mediator,extension);

        var exception = await Record.ExceptionAsync(() => sut.RegisterUser("", ""));

        Assert.Null(exception);

    }

    [Fact]
    public async Task ControllerRegisterUserShouldReturnAStatusCode200()
    {
        var mediator = Substitute.For<IMediator>();
        var extension = Substitute.For<IIpAddressExtensions>();
        var sut = new UsersController(mediator,extension);
        mediator.Send(Arg.Any<AddUserCommand>()).Returns(true);

        var result = await sut.RegisterUser("Alfons", "1234");

        Assert.IsType<OkResult>(result);

    }

    [Fact]
    public async Task ControllerRegisterUserShouldReturnAStatusCode400()
    {
        var mediator = Substitute.For<IMediator>();
        var extension = Substitute.For<IIpAddressExtensions>();
        var sut = new UsersController(mediator,extension);
        mediator.Send(Arg.Any<AddUserCommand>()).Returns(false);

        var result = await sut.RegisterUser("Alfons", "1234");

        Assert.IsType<BadRequestObjectResult>(result);

    }

    [Fact]
    public async Task ControllerAuthenticateUserShouldReturnAStatusCode200()
    {
        var mediator = Substitute.For<IMediator>();
        var extension = Substitute.For<IIpAddressExtensions>();
        var user = UserObjectBuilder.GetUserForAuth();
        var sut = new UsersController(mediator,extension);
        extension.IpAddress(Arg.Any<HttpContext>()).Returns("0.0.0.0");
        mediator.Send(Arg.Any<AuthenticationQuery>()).Returns(new AuthenticateResponse(user, "", ""));

        var result = await sut.AuthenticateUser("Alfons", "1234");

        Assert.IsType<OkObjectResult>(result);

    }

    [Fact]
    public async Task ControllerAuthenticateUserShouldReturnAStatusCode404()
    {
        var mediator = Substitute.For<IMediator>();
        var extension = Substitute.For<IIpAddressExtensions>();
        var sut = new UsersController(mediator, extension);
        extension.IpAddress(Arg.Any<HttpContext>()).Returns("0.0.0.0");
        mediator.Send(Arg.Any<AuthenticationQuery>()).Returns((AuthenticateResponse)null!);

        var result = await sut.AuthenticateUser("Alfons", "1234");

        Assert.IsType<NotFoundResult>(result);

    }

    [Fact]
    public async Task ControllerRefreshTokenShouldReturnAStatusCode400()
    {
        var mediator = Substitute.For<IMediator>();
        var extension = Substitute.For<IIpAddressExtensions>();
        var sut = new UsersController(mediator, extension);
        extension.IpAddress(Arg.Any<HttpContext>()).Returns("0.0.0.0");
        mediator.Send(Arg.Any<RefreshTokenQuery>()).Returns((AuthenticateResponse)null!);

        var result = await sut.RefreshToken("1234");

        Assert.IsType<BadRequestObjectResult>(result);

    }

    [Fact]
    public async Task ControllerRefreshTokenShouldReturnAStatusCode200()
    {
        var mediator = Substitute.For<IMediator>();
        var extension = Substitute.For<IIpAddressExtensions>();
        var user = UserObjectBuilder.GetUserForAuth();
        var sut = new UsersController(mediator, extension);
        extension.IpAddress(Arg.Any<HttpContext>()).Returns("0.0.0.0");
        mediator.Send(Arg.Any<RefreshTokenQuery>()).Returns(new AuthenticateResponse(user,"ddd","ddd"));

        var result = await sut.RefreshToken("1234");

        Assert.IsType<OkObjectResult>(result);

    }

}
