using PVI.GQKN.Infrastructure.Contracts;

namespace PVI.GQKN.Infrastructure.Repositories;

public class RepositoryBaseEntity<TEntity> : RepositoryBase<TEntity>, IPageListRepository<TEntity>
    where TEntity : Entity, IAggregateRoot
{
    public RepositoryBaseEntity(GQKNDbContext context) : base(context)
    {
    }

    public virtual IQueryable<TEntity> ApplyQuery(PagedListQueryParams r, ref IQueryable<TEntity> q)
    {
        throw new NotImplementedException();
    }

    public override bool DeleteByGuid(string guid)
    {
        var item = GetByGuidAsync(guid).Result;
        if (item != null)
        {
            this.Delete(item);
            return true;
        }
        return false;
    }

    public override async Task<bool> DeleteByGuidAsync(string guid)
    {
        var item = await GetByGuidAsync(guid);
        if (item != null)
        {
            Delete(item);
            return true;
        }
        return false;
    }

    public override async Task<TEntity> GetByGuidAsync(string guid)
    {
        if (string.IsNullOrEmpty(guid))
            return null;

        if (Guid.TryParse(guid, out Guid id))
        {
            return await dbSet.FirstOrDefaultAsync(e => e.Guid == id);
        }

        return null;
    }

    public virtual async Task<PagedList<TEntity>> GetPage(PagedListQueryParams request, string includeProperties = "")
    {
        var source = this.GetAll();
        var pageId = request.PageId ?? 0;

        source = ApplyQuery(request, ref source)
            .Where(b => b.Id > pageId);

        source = IncludeProperties(source, includeProperties);

        var count = await source.CountAsync();

        var items = await source
            .Take(request.PageSize).ToListAsync();

        int? id = items.Count == request.PageSize ? items.Last().Id : null;

        return new PagedList<TEntity>(items, count, request.PageSize, request.PageId, id);
    }


}