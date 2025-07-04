using Api.Api.DTOs;
using Api.Application.Commands.AddItem;
using Api.Application.Queries.GetItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Api.Controllers;

[Route("/api/v1/items")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddItem([FromBody] AddItemRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new AddItemCommand(request.Name, request.Description, request.Price), cancellationToken);

        return CreatedAtAction(nameof(AddItem), response);
    }

    [HttpGet]
    public async Task<IActionResult> GetItems(CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new GetItemsQuery(), cancellationToken));
    }
}
