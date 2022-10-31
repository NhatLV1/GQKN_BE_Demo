namespace PVI.GQKN.Infrastructure.Contracts;

public interface IApplicationRoleRepository: IRepository<ApplicationRole>
{
    Task<IEnumerable<ApplicationRole>> FindRolesWithNames(IEnumerable<string> names);
}
