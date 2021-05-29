using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProyectoFinal.DAL.Repositories.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> where = null, string includes = "",
            Expression<Func<T, object>> orderBy = null);

        Task<T> GetById(Guid id, string includes = "");

        Task<T> GetByCondition(Expression<Func<T, bool>> where, string includes = "");

        ValueTask<T> Create(T entity);

        Task<bool> Update(T entity);

        Task<bool> Delete(T entity);
    }
}