using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TableStorage.Services
{
    public interface INoSQLStorage<T>
    {
        Task<T> Add(T entity);
        Task<T> Get(string rowKey, string partitionKey);
        Task<T> Delete(string rowKey, string partitionKey);
        Task<T> Update(T entity);
        IQueryable<T> All();
        IQueryable<T> Query(Expression<Func<T, bool>> query);
    }
}
