using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.IRepository
{

  public interface IIdentityRepository<T>
  {
    Task<T> GetByIdAsync(string id);
    Task<IEnumerable<T>> ListAllAsync();
    //Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    List<T> GetByFilter(Func<T, bool> filter = null);
    Task<T> AddAsync(T entity);
    int AddRange(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    int DeleteRange(IEnumerable<T> entities);
    //Task<int> CountAsync(ISpecification<T> spec);
  }
}
