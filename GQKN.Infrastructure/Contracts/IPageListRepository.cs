namespace PVI.GQKN.Infrastructure.Contracts;

public interface IPageListRepository<T>: IRepository<T>
    where T : IAggregateRoot
{
    public Task<PagedList<T>> GetPage(PagedListQueryParams request, string includeProperties = "");

    public IQueryable<T> ApplyQuery(PagedListQueryParams r, ref IQueryable<T> q);
}
