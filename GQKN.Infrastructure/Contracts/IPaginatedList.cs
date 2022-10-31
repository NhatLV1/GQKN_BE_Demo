namespace PVI.GQKN.Infrastructure.Contracts;

public interface IPaginatedList
{
    int TotalPage { get; }
    int PageIndex { get; }
    int PageSize { get; }
    int Count { get; }
    int TotalCount { get; }

    bool HasNextPage { get; }
    bool HasPreviousPage { get; }
}
