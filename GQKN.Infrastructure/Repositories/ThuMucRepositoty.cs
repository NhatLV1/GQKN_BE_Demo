using PVI.GQKN.Infrastructure.Repositories;
using PVI.GQKN.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVI.GQKN.Infrastructure.Repositories;

public class ThuMucRepositoty :
     RepositoryBaseEntity<ThuMuc>, IThuMucRepositoty
{
    public ThuMucRepositoty(GQKNDbContext context) : base(context)
    {
    }

    public override IQueryable<ThuMuc> ApplyQuery(PagedListQueryParams r, ref IQueryable<ThuMuc> q)
    {
        //TODO: query
        return q;
    }
}
