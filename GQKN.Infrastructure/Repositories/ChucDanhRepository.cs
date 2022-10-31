using PVI.GQKN.Infrastructure.Contracts;

namespace PVI.GQKN.Infrastructure.Repositories;

public class ChucDanhRepository : RepositoryBaseEntity<ChucDanh>, IChucDanhRepository
{
    public ChucDanhRepository(GQKNDbContext context) : base(context)
    {
    }
}
