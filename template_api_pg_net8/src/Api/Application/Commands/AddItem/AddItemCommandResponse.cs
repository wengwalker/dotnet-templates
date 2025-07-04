namespace Api.Application.Commands.AddItem;

public record AddItemCommandResponse(
    Guid Id,
    string Name,
    string? Description,
    decimal Price);