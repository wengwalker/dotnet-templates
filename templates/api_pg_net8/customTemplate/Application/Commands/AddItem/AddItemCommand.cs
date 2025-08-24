using Api.Infrastructure.MediatR.Interfaces;

namespace Api.Application.Commands.AddItem;

public class AddItemCommand : IRequest<AddItemCommandResponse>
{
    public string Name { get; init; }

    public string? Description { get; init; }

    public decimal Price { get; init; }

    public AddItemCommand(string name, string? description, decimal price)
    {
        this.Name = name;
        this.Description = description;
        this.Price = price;
    }
}
