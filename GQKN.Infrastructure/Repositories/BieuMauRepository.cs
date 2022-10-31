using PVI.GQKN.Infrastructure.Contracts;
using PVI.GQKN.Infrastructure.Idempotency;

namespace PVI.GQKN.Infrastructure.Repositories;

public class BieuMauRepository : RepositoryBaseEntity<BieuMau>, IBieuMauRepository
{
    public BieuMauRepository(GQKNDbContext context) : base(context)
    {
    }
}
