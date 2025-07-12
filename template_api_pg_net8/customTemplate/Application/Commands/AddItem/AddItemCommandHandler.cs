using Api.Api.Extensions;
using Api.Domain.Entities;
using Api.Infrastructure.Data.Contexts;
using Api.Infrastructure.MediatR.Interfaces;
using FluentValidation;

namespace Api.Application.Commands.AddItem;

public sealed class AddItemCommandHandler : IRequestHandler<AddItemCommand, AddItemCommandResponse>
{
    private readonly ItemsDbContext _context;

    private readonly IValidator<AddItemCommand> _validator;

    public AddItemCommandHandler(ItemsDbContext context, IValidator<AddItemCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<AddItemCommandResponse> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowExceptionAsync(request, "Invalid AddItemCommand model", cancellationToken);

        var newItem = new ItemEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
        };

        await _context.Items.AddAsync(newItem, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new AddItemCommandResponse(newItem.Id, newItem.Name, newItem.Description, newItem.Price);
    }
}
