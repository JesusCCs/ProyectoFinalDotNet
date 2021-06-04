using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> where = null, string includes = "",
            Expression<Func<T, object>> orderBy = null, string order = "desc")
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes.Split(",", StringSplitOptions.RemoveEmptyEntries))
            {
                query.Include(include);
            }

            if (where is not null)
            {
                query = query.Where(where);
            }

            if (orderBy is not null)
            {
                query = order == "desc" ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetById(Guid id, string includes = "")
        {
            IQueryable<T> query = _context.Set<T>();

            query = includes.Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, include) => current.Include(include));

            return await query.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<T> GetByCondition(Expression<Func<T, bool>> where, string includes = "")
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes.Split(",", StringSplitOptions.RemoveEmptyEntries))
            {
                query.Include(include);
            }

            return await query.Where(where).FirstOrDefaultAsync();
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
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            var entry = _context.Entry(entity);

            entry.Entity.FechaActualizado = DateTime.Now;

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (property.GetValue(entity, null) != null) continue;

                try
                {
                    entry.Property(property.Name).IsModified = false;
                }
                catch (InvalidOperationException)
                {
                }
            }

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