using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeApi.Application.Common.Models;
using RecipeApi.Application.Common.Models.UserRecipes;
using RecipeApi.Application.Recipes.Commands;
using RecipeApi.Application.Recipes.Queries.GetRandomRecipies;
using RecipeApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeApi.WebAppApi.Controllers;
[Authorize]
[ApiController]
[Route("/api/v1/[controller]")]
public class RecipeController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RecipeController> _logger;

    public RecipeController(IMediator mediator, ILogger<RecipeController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    /// <summary>
    /// Asks the api to get random recipes based on preferences
    /// </summary>
    /// <param name="mealType"></param>
    /// <param name="query"></param>
    /// <returns>List of random recipes</returns>

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RecipeViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery] MealType mealType, PreferenceType preference,string allergies)
    {

        var response = await _mediator.Send(new GetRecipesQuery(mealType, preference,allergies));

        if (response is null)
            return NotFound();

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> PostAsync([FromQuery] UserRecipeRequest request)
    {
        await _mediator.Send(new AddRecipeCommand(request));

        return Accepted();
    }

}
