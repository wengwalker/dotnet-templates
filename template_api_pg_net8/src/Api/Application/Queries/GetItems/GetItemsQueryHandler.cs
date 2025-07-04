using Api.Domain.DTOs;
using Api.Infrastructure.Data.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Application.Queries.GetItems;

public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, GetItemsQueryResponse>
{
    private readonly ItemsDbContext _context;

    public GetItemsQueryHandler(ItemsDbContext context)
    {
        _context = context;
    }

    public async Task<GetItemsQueryResponse> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        var itemsList = await _context.Items
            .Select(x => new ItemDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
            })
            .ToListAsync(cancellationToken);

        return new GetItemsQueryResponse(itemsList);
    }
}
