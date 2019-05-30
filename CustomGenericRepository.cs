using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data.Services
{
    public class CustomGenericRepository<TEntity> where TEntity : class
    {
        private ApplicationDbContext _context;
        private DbSet<TEntity> _dbset;

        public CustomGenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbset = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get
            (Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, string includes = "")
        {
            IQueryable<TEntity> query = _dbset;

            if (where != null)
            {
                query = query.Where(where);
            }
            if (orderby != null)
            {
                query = orderby(query);
            }
            if (includes != "")
            {
                foreach (var include in includes.Split(","))
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }

        public virtual Task<List<TEntity>> GetAsync
            (Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, string includes = "", int? skip = null, int? take = null)//, List<Expression<Func<TEntity, object>>> expressions=null)
        {
            IQueryable<TEntity> query = _dbset;

            if (where != null)
            {
                query = query.Where(where);
            }
            if (orderby != null)
            {
                query = orderby(query);
            }
            if (includes != "")
            {
                foreach (var include in includes.Split(","))
                {
                    query = query.Include(include);
                }
            }
            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }


            return query.AsNoTracking().ToListAsync();
        }



        public virtual Task<TEntity> GetById(object id)
        {
            return _dbset.FindAsync(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbset.AddAsync(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbset.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbset.Attach(entity);
            }
            _dbset.Remove(entity);
        }

        public virtual void Delete(object id)
        {
            var entity = GetById(id);
            Delete(entity);
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> where = null)
        {
            IQueryable<TEntity> query = _dbset;
            if (where != null)
            {
                query = query.Where(where);
            }

            return query.CountAsync();
        }

        public virtual async Task<TEntity> InsertAndGet(TEntity entity)
        {
            await _dbset.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task InsertRange(IEnumerable<TEntity> entities)
        {
            await _dbset.AddRangeAsync(entities);
        }

        // better to make it in unit of work
        //public virtual void Save()
        //{
        //    _context.SaveChangesAsync();
        //}

    }

    // Another Generic Repository Sample

    //public interface IGenericRepository<TEntity> where TEntity : class
    //{
    //    TEntity Add(TEntity t);
    //    Task<TEntity> AddAsyn(TEntity t);
    //    int Count();
    //    Task<int> CountAsync();
    //    void Delete(TEntity entity);
    //    Task<int> DeleteAsyn(TEntity entity);
    //    void Dispose();
    //    TEntity Find(Expression<Func<TEntity, bool>> match);
    //    ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match);
    //    Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);
    //    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);
    //    IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
    //    Task<ICollection<TEntity>> FindByAsyn(Expression<Func<TEntity, bool>> predicate);
    //    TEntity Get(int id);
    //    IQueryable<TEntity> GetAll();
    //    Task<ICollection<TEntity>> GetAllAsyn();
    //    IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
    //    Task<TEntity> GetAsync(int id);
    //    void Save();
    //    Task<int> SaveAsync();
    //    TEntity Update(TEntity t, object key);
    //    Task<TEntity> UpdateAsyn(TEntity t, object key);
    //}

    //public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    //{
    //    protected ApplicationDbContext _context;
    //    public GenericRepository(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }

    //    public IQueryable<TEntity> GetAll()
    //    {
    //        return _context.Set<TEntity>();
    //    }

    //    public virtual async Task<ICollection<TEntity>> GetAllAsyn()
    //    {

    //        return await _context.Set<TEntity>().ToListAsync();
    //    }

    //    public virtual TEntity Get(int id)
    //    {
    //        return _context.Set<TEntity>().Find(id);
    //    }

    //    public virtual async Task<TEntity> GetAsync(int id)
    //    {
    //        return await _context.Set<TEntity>().FindAsync(id);
    //    }

    //    public virtual TEntity Add(TEntity t)
    //    {

    //        _context.Set<TEntity>().Add(t);
    //        _context.SaveChanges();
    //        return t;
    //    }

    //    public virtual async Task<TEntity> AddAsyn(TEntity t)
    //    {
    //        _context.Set<TEntity>().Add(t);
    //        await _context.SaveChangesAsync();
    //        return t;

    //    }

    //    public virtual TEntity Find(Expression<Func<TEntity, bool>> match)
    //    {
    //        return _context.Set<TEntity>().SingleOrDefault(match);
    //    }

    //    public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
    //    {
    //        return await _context.Set<TEntity>().SingleOrDefaultAsync(match);
    //    }

    //    public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
    //    {
    //        return _context.Set<TEntity>().Where(match).ToList();
    //    }

    //    public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
    //    {
    //        return await _context.Set<TEntity>().Where(match).ToListAsync();
    //    }

    //    public virtual void Delete(TEntity entity)
    //    {
    //        _context.Set<TEntity>().Remove(entity);
    //        _context.SaveChanges();
    //    }

    //    public virtual async Task<int> DeleteAsyn(TEntity entity)
    //    {
    //        _context.Set<TEntity>().Remove(entity);
    //        return await _context.SaveChangesAsync();
    //    }

    //    public virtual TEntity Update(TEntity t, object key)
    //    {
    //        if (t == null)
    //            return null;
    //        TEntity exist = _context.Set<TEntity>().Find(key);
    //        if (exist != null)
    //        {
    //            _context.Entry(exist).CurrentValues.SetValues(t);
    //            _context.SaveChanges();
    //        }
    //        return exist;
    //    }

    //    public virtual async Task<TEntity> UpdateAsyn(TEntity t, object key)
    //    {
    //        if (t == null)
    //            return null;
    //        TEntity exist = await _context.Set<TEntity>().FindAsync(key);
    //        if (exist != null)
    //        {
    //            _context.Entry(exist).CurrentValues.SetValues(t);
    //            await _context.SaveChangesAsync();
    //        }
    //        return exist;
    //    }

    //    public int Count()
    //    {
    //        return _context.Set<TEntity>().Count();
    //    }

    //    public async Task<int> CountAsync()
    //    {
    //        return await _context.Set<TEntity>().CountAsync();
    //    }

    //    public virtual void Save()
    //    {

    //        _context.SaveChanges();
    //    }

    //    public async virtual Task<int> SaveAsync()
    //    {
    //        return await _context.SaveChangesAsync();
    //    }

    //    public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
    //    {
    //        IQueryable<TEntity> query = _context.Set<TEntity>().Where(predicate);
    //        return query;
    //    }

    //    public virtual async Task<ICollection<TEntity>> FindByAsyn(Expression<Func<TEntity, bool>> predicate)
    //    {
    //        return await _context.Set<TEntity>().Where(predicate).ToListAsync();
    //    }

    //    public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
    //    {

    //        IQueryable<TEntity> queryable = GetAll();
    //        foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
    //        {

    //            queryable = queryable.Include<TEntity, object>(includeProperty);
    //        }

    //        return queryable;
    //    }

    //    private bool disposed = false;
    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (!this.disposed)
    //        {
    //            if (disposing)
    //            {
    //                _context.Dispose();
    //            }
    //            this.disposed = true;
    //        }
    //    }

    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }
    //}

}
