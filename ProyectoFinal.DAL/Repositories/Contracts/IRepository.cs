using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoFinal.DAL.Repositories.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(Guid id, string includes = "");
        
        ValueTask<T> Create(T entity);
        
        Task<bool> Update(T entity);
        
        Task<bool> Delete(T entity);
    }
}