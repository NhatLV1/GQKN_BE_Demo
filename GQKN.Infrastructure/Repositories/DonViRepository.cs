using PVI.GQKN.Infrastructure.Contracts;

namespace PVI.GQKN.Infrastructure.Repositories;

public class DonViRepository :
    RepositoryBaseEntity<DonVi>, IDonViRepository
{
    public DonViRepository(GQKNDbContext context) : base(context)
    {
    }
}
