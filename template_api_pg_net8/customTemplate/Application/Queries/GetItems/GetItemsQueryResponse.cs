using Api.Domain.DTOs;

namespace Api.Application.Queries.GetItems;

public record GetItemsQueryResponse(List<ItemDto> Items);
