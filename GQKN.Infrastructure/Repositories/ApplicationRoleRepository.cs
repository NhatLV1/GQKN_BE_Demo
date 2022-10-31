using Microsoft.EntityFrameworkCore;

namespace PVI.GQKN.Infrastructure.Repositories;

public class ApplicationRoleRepository : RepositoryBase<ApplicationRole>,
    IApplicationRoleRepository
{
    public ApplicationRoleRepository(GQKNDbContext context) : base(context)
    {
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

    public async Task<IEnumerable<ApplicationRole>> FindRolesWithNames(IEnumerable<string> names)
    {
        var results = await this.FindByCondition(e => names.Contains(e.Name))
            .ToListAsync();

        return results;
    }

    public override async Task<ApplicationRole> GetByGuidAsync(string guid)
    {
        if (string.IsNullOrEmpty(guid))
            return null;

        return await dbSet.FindAsync(guid);
    }

}
