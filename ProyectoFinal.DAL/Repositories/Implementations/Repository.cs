using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.DAL.Models;
using ProyectoFinal.DAL.Repositories.Contracts;

namespace ProyectoFinal.DAL.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : Base
    {
        private readonly DataBaseContext _context;

        public Repository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> Get(Guid id, string includes = "")
        {
            IQueryable<T> query = _context.Set<T>();
            
            foreach (var include in includes.Split(",",StringSplitOptions.RemoveEmptyEntries))
            {
                query.Include(include);
            }
            
            return await query.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async ValueTask<T> Create(T entity)
        {
            entity.Id = Guid.NewGuid();
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            
            return entity;
        }

        public async Task<bool> Update(T entity)
        {
            var element = await _context.Set<T>().FindAsync(entity.Id);
            _context.Entry(element).CurrentValues.SetValues(element);
            var count = await _context.SaveChangesAsync();
            
            return count == 1;
        }

        public async Task<bool> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            var count = await _context.SaveChangesAsync();

            return count == 1;
        }
    }
}