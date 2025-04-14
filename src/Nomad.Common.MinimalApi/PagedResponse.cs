namespace Nomad.Common.MinimalApi;

/// <summary>
///     Represents a paginated API response.
/// </summary>
/// <typeparam name="T">The type of the items in the page.</typeparam>
public sealed record PagedResponse<T>(
    IReadOnlyList<T> Items,
    int TotalCount,
    int PageSize,
    int PageNumber)
{
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}
