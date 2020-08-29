using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore
{
  public interface IGenericRepositry<T> where T : class
    {
    Task<object> InsertAsync(T entity);
    IEnumerable<T> GetAll();
    Task<T> GetByIdAsync(long id);
    void Delete(long id);
    void Update(T entity);
     EntityEntry<T> Remove(T entity);
    }
}
