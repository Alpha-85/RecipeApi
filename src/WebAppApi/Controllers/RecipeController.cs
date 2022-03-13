using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeApi.Application.Common.Models;
using RecipeApi.Application.Common.Models.Recipes;
using RecipeApi.Application.Common.Models.UserRecipes;
using RecipeApi.Application.Recipes.Commands;
using RecipeApi.Application.Recipes.Queries.GetRandomRecipes;
using RecipeApi.Application.Recipes.Queries.GetRecipe;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeApi.WebAppApi.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class RecipeController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RecipeController> _logger;

    public RecipeController(IMediator mediator, ILogger<RecipeController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RecipeViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] RecipeRequest recipeRequest)
    {

        var response = await _mediator.Send(new GetRecipesQuery(recipeRequest));

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync([FromQuery] UserRecipeRequest request)
    {
        var response = await _mediator.Send(new AddRecipeCommand(request));

        if (response is false)
            return BadRequest("Invalid data/combination");

        return Ok("Saved to db");
    }

    [HttpGet("oneRecipe")]
    [ProducesResponseType(typeof(RecipeViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRecipe([FromQuery] long recipeId)
    {

        var response = await _mediator.Send(new GetRecipeQuery(recipeId));

        return Ok(response);
    }

}
