using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeApi.Application.Common.Models;
using RecipeApi.Application.Recipes.Commands.AddRecipeCollection;
using System;
using System.Threading.Tasks;
using RecipeApi.Application.Recipes.Queries.GetRecipeCollections;

namespace RecipeApi.WebAppApi.Controllers;

[Authorize]
[ApiController]
[Route("/api/v1/[controller]")]
public class RecipeCollectionController : ControllerBase
{
    private readonly IMediator _mediator;
    public RecipeCollectionController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    [ProducesResponseType(typeof(RecipeCollectionViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> PostAsync([FromQuery] int userId, string collectionName)
    {

        var response = await _mediator.Send(new AddRecipeCollectionCommand(collectionName, userId));

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(RecipeCollectionViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromQuery] int userId)
    {

        var response = await _mediator.Send(new GetRecipeCollectionQuery(userId));

        return Ok(response);
    }
}
