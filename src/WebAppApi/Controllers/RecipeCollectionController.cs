using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeApi.Application.Common.Models;
using RecipeApi.Application.Recipes.Commands.AddRecipeCollection;
using System;
using System.Threading.Tasks;
using RecipeApi.Application.Common.Models.Recipes.RecipeResponse;
using RecipeApi.Application.Recipes.Commands.DeleteRecipeCollection;
using RecipeApi.Application.Recipes.Queries.GetRecipeCollections;
using RecipeApi.Application.Recipes.Queries.GetRecipeIngredients;

namespace RecipeApi.WebAppApi.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class RecipeCollectionController : ControllerBase
{
    private readonly IMediator _mediator;
    public RecipeCollectionController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    [ProducesResponseType(typeof(RecipeCollectionResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> PostAsync([FromQuery] int userId, string collectionName)
    {

        var response = await _mediator.Send(new AddRecipeCollectionCommand(collectionName, userId));

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(RecipeCollectionResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromQuery] int userId)
    {

        var response = await _mediator.Send(new GetRecipeCollectionQuery(userId));

        return Ok(response);
    }

    [HttpGet("Details")]
    [ProducesResponseType(typeof(RecipeDayResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> DetailsAsync([FromQuery] int userId, int collectionId)
    {

        var response = await _mediator.Send(new GetRecipeDayQuery(userId,collectionId));

        return Ok(response);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromQuery] int userId, int collectionId)
    {
        var response = await _mediator.Send(new DeleteRecipeCollectionCommand(userId, collectionId));
        if (response is false)
            return NotFound();

        return Ok("Collection deleted");

    }

    [HttpGet("ShoppingList")]
    [ProducesResponseType(typeof(RecipeShoppingListResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetShoppingListAsync([FromQuery] int userId, int collectionId)
    {

        var response = await _mediator.Send(new GetShoppingIngredientsQuery(userId, collectionId));

        return Ok(response);
    }
}
