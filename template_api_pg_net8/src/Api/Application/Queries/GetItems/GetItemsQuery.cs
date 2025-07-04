using MediatR;

namespace Api.Application.Queries.GetItems;

public record GetItemsQuery() : IRequest<GetItemsQueryResponse>;
