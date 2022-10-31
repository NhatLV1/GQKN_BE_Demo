

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PVI.GQKN.Infrastructure.Repositories;

public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
    where TEntity : class, IAggregateRoot
{
    internal GQKNDbContext context;
    internal DbSet<TEntity> dbSet;

    public IUnitOfWork UnitOfWork => context;

    public RepositoryBase(GQKNDbContext context)
    {
        this.context = context;
        this.dbSet = context.Set<TEntity>();
    }

    public IQueryable<TEntity> GetAll(string includeProperties = "")
    {
        var query = this.dbSet
            .AsNoTracking();

        foreach (var includeProperty in includeProperties.Split
           (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return query;
    }

    protected IQueryable<TEntity> IncludeProperties( IQueryable<TEntity> query, string includeProperties = "")
    {
        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return query;
    }

    public virtual IQueryable<TEntity> FindByCondition(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        query = IncludeProperties(query, includeProperties);
        //foreach (var includeProperty in includeProperties.Split
        //    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //{
        //    query = query.Include(includeProperty);
        //}

        if (orderBy != null)
        {
            return orderBy(query);
        }
        else
        {
            return query;
        }
    }

    public virtual TEntity GetByID(int id)
    {
        return dbSet.Find(id);
    }

    public abstract Task<TEntity> GetByGuidAsync(string guid);

    public virtual TEntity Insert(TEntity entity)
    {
        return dbSet.Add(entity).Entity;
    }

    public virtual bool Delete(int id)
    {
        TEntity entityToDelete = dbSet.Find(id);
        if(entityToDelete != null)
            Delete(entityToDelete);
        return entityToDelete != null;
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (context.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }
        dbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        dbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public abstract bool DeleteByGuid(string guid);

    public abstract Task<bool> DeleteByGuidAsync(string guid);

    public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, 
        string orderBy = null, string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            //return orderBy(query);
            return query.OrderBy(orderBy);
        }
        else
        {
            return query;
        }
    }

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
    {
        var result = await FindByCondition(filter, null, includeProperties).FirstOrDefaultAsync();
        return result;
    }

    public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool enableTracking = true, int offset = 0, int limit = -1)
    {
        IQueryable<TEntity> query = dbSet;
        if (!enableTracking) query = query.AsNoTracking();

        if (filter != null)
            query = query.Where(filter);

        if (include != null) query = include(query);

        if (orderBy != null)
            return orderBy(query);

        //limit = -1: get all data
        if (limit != -1)
        {
            return query.Skip(offset).Take(limit);
        }

        return query;
    }
}
