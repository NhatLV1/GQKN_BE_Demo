using PVI.GQKN.Infrastructure.Contracts;

namespace PVI.GQKN.Infrastructure.Repositories;

public class PhongBanRepository :
     RepositoryBaseEntity<PhongBan>, IPhongBanRepository
{
    public PhongBanRepository(GQKNDbContext context) : base(context)
    {
    }

    public override IQueryable<PhongBan> ApplyQuery(PagedListQueryParams r, ref IQueryable<PhongBan> q)
    {
        //TODO: query
        return q;
    }
}
