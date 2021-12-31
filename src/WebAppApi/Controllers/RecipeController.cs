using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeApi.Application.Recipes.Queries.GetRandomRecipies;
using System;
using System.Threading.Tasks;

namespace RecipeApi.WebAppApi.Controllers;
[ApiController]
[Route("/api/v1[controller]")]
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
    /// <param name="mainIngredient"></param>
    /// <returns>List of random recipes</returns>

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery] string mealType, string mainIngredient)
    {

        var recipeList = await _mediator.Send(new GetRecipesQuery(mealType, mainIngredient));

        if (recipeList is null)
            return NotFound();

        return Ok(recipeList);
    }
}
