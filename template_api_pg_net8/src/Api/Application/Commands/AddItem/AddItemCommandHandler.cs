using Api.Domain.Entities;
using Api.Infrastructure.Data.Contexts;
using Api.Infrastructure.MediatR.Interfaces;

namespace Api.Application.Commands.AddItem;

public sealed class AddItemCommandHandler : IRequestHandler<AddItemCommand, AddItemCommandResponse>
{
    private readonly ItemsDbContext _context;

    public AddItemCommandHandler(ItemsDbContext context)
    {
        _context = context;
    }

    public Task<AddItemCommandResponse> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var newItem = new ItemEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
        };

        _context.Items.AddAsync(newItem, cancellationToken);

        _context.SaveChangesAsync(cancellationToken);

        return Task.FromResult(new AddItemCommandResponse(newItem.Id, newItem.Name, newItem.Description,
          newItem.Price));
    }
}
