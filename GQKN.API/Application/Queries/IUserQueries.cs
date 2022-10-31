namespace PVI.GQKN.API.Application.Queries;

public interface IUserQueries
{
    public Task<IEnumerable<ApplicationUser>> GetAll();
    
    public Task<ApplicationUser> GetById(string id);


}
