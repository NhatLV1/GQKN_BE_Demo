namespace PVI.GQKN.API.Application.Queries;

public class UserQueries : IUserQueries
{
    private readonly UserManager<ApplicationUser> userManager;

    public UserQueries(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<IEnumerable<ApplicationUser>> GetAll()
    {
        return await userManager.Users.ToListAsync();
    }

    public Task<ApplicationUser> GetById(string id)
    {
        return userManager.FindByIdAsync(id);
    }
}
