using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeApi.Application.Common.Models.Authentication;
using RecipeApi.Application.Users.Commands;
using RecipeApi.Application.Users.Queries.Authentication;
using System;
using System.Threading.Tasks;

namespace RecipeApi.WebAppApi.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{

    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    }

    /// <summary>
    /// Authenticates a registered user
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>

    [AllowAnonymous]
    [HttpPost("authenticate")]
    [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AuthenticateUser(string username, string password)
    {
        var response = await _mediator.Send(new AuthenticationQuery(username, password, IpAddress()));

        if (response is null)
            return NotFound();

        //SetTokenCookie(response.RefreshToken);

        return Ok(response);
    }

    /// <summary>
    /// Registers a new user
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>

    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterUser(string username, string password)
    {
        var response = await _mediator.Send(new AddUserCommand(username, password));

        if (response is false)
            return BadRequest("Something went wrong");

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody]string refreshToken)
    {

        var response = await _mediator.Send(new RefreshTokenQuery(refreshToken, IpAddress()));

        if (response is null)
            return BadRequest(new { message = "Something is wrong with RefreshToken" });

        //SetTokenCookie(response.RefreshToken);
        return Ok(response);
    }

    private void SetTokenCookie(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7)
        };
        Response.Cookies.Append("refreshToken", token, cookieOptions);
    }

    private string IpAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        else
            return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }
}
