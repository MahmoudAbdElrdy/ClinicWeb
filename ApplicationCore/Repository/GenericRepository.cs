using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repository
{

  public class GenericRepository<T> : IGenericRepositry<T> where T : class
  {
    private readonly ApiContext apiContext;


    public GenericRepository(ApiContext _apiContext)
    {
      apiContext = _apiContext;
    }

    public void Delete(long id)
    {
      T existing = apiContext.Set<T>().Find(id);
      apiContext.Set<T>().Remove(existing);
     // apiContext.SaveChanges();
    }
        public virtual EntityEntry<T> Remove(T entity)
        {
            return apiContext.Set<T>().Remove(entity);
        }
        public IEnumerable<T> GetAll()
    {
      return apiContext.Set<T>().ToList();
    }

    public async Task<T> GetByIdAsync(long id)
    {
      var entity = await apiContext.Set<T>().FindAsync(id);
      apiContext.Entry(entity).State = EntityState.Detached;
      return entity;
    }

    public async Task<object> InsertAsync(T entity)
    {
      try
      {
        await apiContext.Set<T>().AddAsync(entity);
        //apiContext.SaveChanges();
        return entity;
      }
      catch (Exception ex)
      {
        return ex.Message;
      }
    }

    public void Update(T entity)
    {
      apiContext.Set<T>().Attach(entity);
      apiContext.Entry(entity).State = EntityState.Modified;
    //  apiContext.SaveChanges();
    }

  }
}
