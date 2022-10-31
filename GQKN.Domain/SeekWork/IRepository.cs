using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PVI.GQKN.Domain.Models;
using PVI.GQKN.Domain.Models.Identity;
using System.Linq.Expressions;

namespace PVI.GQKN.Domain.Seedwork;

public interface IRepository<T> where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }

    T GetByID(int id);

    Task<T> GetByGuidAsync(string guid);

    Task<T> GetByIdAsync(int id);

    T Insert(T entity);

    bool Delete(int id);

    bool DeleteByGuid(string guid);

    Task<bool> DeleteByGuidAsync(string guid);

    void Delete(T entityToDelete);

    void Update(T entityToUpdate);

    IQueryable<T> GetAll(string includeProperties = "");

    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "");

    IQueryable<T> FindByCondition(Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "");

    IQueryable<T> Query(Expression<Func<T, bool>> filter = null,
    string orderBy = null,
    string includeProperties = "");
    IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool enableTracking = true, int offset = 0, int limit = -1);
}
