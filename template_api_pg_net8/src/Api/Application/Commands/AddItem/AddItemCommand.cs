using MediatR;

namespace Api.Application.Commands.AddItem;

public record AddItemCommand(
    string Name,
    string? Description,
    decimal Price)
    : IRequest<AddItemCommandResponse>;
