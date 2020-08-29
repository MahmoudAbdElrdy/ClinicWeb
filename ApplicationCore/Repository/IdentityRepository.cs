using ApplicationCore.IRepository;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repository
{
  public class IdentityRepository<T> : IIdentityRepository<T> where T : class
  {
    protected readonly ApiContext _dbContext;

    public IdentityRepository(ApiContext dbContext)
    {
      _dbContext = dbContext;
    }

    public virtual async Task<T> GetByIdAsync(string id)
    {
      return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> ListAllAsync()
    {
      return await _dbContext.Set<T>().ToListAsync();
    }

    public List<T> GetByFilter(Func<T, bool> filter = null)
    {
      return  _dbContext.Set<T>().Where(filter ?? (s => true)).ToList();
    }

    //public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    //{
    //    return await ApplySpecification(spec).ToListAsync();
    //}

    //public async Task<int> CountAsync(ISpecification<T> spec)
    //{
    //    return await ApplySpecification(spec).CountAsync();
    //}

    public async Task<T> AddAsync(T entity)
    {
      await _dbContext.Set<T>().AddAsync(entity);
      await _dbContext.SaveChangesAsync();

      return entity;
    }

    public int AddRange(IEnumerable<T> entities)
    {
      _dbContext.Set<T>().AddRange(entities);
      return _dbContext.SaveChanges();
    }

    public async Task UpdateAsync(T entity)
    {
      _dbContext.Entry(entity).State = EntityState.Modified;
      await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
      _dbContext.Set<T>().Remove(entity);
      await _dbContext.SaveChangesAsync();
    }

    //private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    //{
    //    return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
    //}

    public int DeleteRange(IEnumerable<T> entities)
    {
      _dbContext.Set<T>().RemoveRange(entities);
      return _dbContext.SaveChanges();
    }
  }
}
