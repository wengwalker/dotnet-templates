namespace Api.Api.DTOs;

public record AddItemRequest(
    string Name,
    string? Description,
    decimal Price);