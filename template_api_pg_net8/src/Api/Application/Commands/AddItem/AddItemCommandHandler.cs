using Api.Domain.Entities;
using Api.Infrastructure.Data.Contexts;
using MediatR;

namespace Api.Application.Commands.AddItem;

public sealed class AddItemCommandHandler : IRequestHandler<AddItemCommand, AddItemCommandResponse>
{
    private readonly ItemsDbContext _context;

    public AddItemCommandHandler(ItemsDbContext context)
    {
        _context = context;
    }

    public async Task<AddItemCommandResponse> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var newItem = new ItemEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
        };

        await _context.Items.AddAsync(newItem);

        await _context.SaveChangesAsync(cancellationToken);

        return new AddItemCommandResponse(newItem.Id, newItem.Name, newItem.Description, newItem.Price);
    }
}
