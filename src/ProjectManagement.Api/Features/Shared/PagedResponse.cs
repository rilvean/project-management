namespace ProjectManagement.Api.Features.Shared;

public sealed record PagedResponse<T>(
    IReadOnlyList<T> Items,
    int TotalCount,
    int Page,
    int PageSize
);