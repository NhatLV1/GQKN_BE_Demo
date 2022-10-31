namespace PVI.GQKN.Infrastructure.Contracts;

/// <summary>
/// https://learn.microsoft.com/en-us/ef/core/querying/pagination
/// <br/>Unfortunately, while this technique is very intuitive, it also has some severe shortcomings:
/// <br/>The database must still process the first 20 entries, even if they aren't returned to the application; this creates possibly significant computation load that increases with the number of rows being skipped.
/// <br/>If any updates occur concurrently, your pagination may end up skipping certain entries or showing them twice.For example, if an entry is removed as the user is moving from page 2 to 3, the whole resultset "shifts up", and one entry would be skipped.
/// </summary>
/// <typeparam name="T"></typeparam>
public class PaginationListed<T> : List<T>, IPaginatedList
    where T : Entity
{
    public int TotalPage { get; private set; }
    public int PageIndex { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public PaginationListed(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPage = (int)Math.Ceiling(count / (double)pageSize);
        PageSize = pageSize;
        TotalCount = count;
        AddRange(items);
    }
    public bool HasNextPage
    {
        get
        {
            return PageIndex < TotalPage;
        }
    }

    public bool HasPreviousPage
    {
        get
        {
            return PageIndex > 1;
        }
    }

    public static async Task<PaginationListed<T>> CreateAsync(IQueryable<T> source,
        int pageIndex, int pageSize = 30)
    {
        var count = await source.CountAsync();
        var items = await source
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginationListed<T>(items, count, pageIndex, pageSize);
    }

}
