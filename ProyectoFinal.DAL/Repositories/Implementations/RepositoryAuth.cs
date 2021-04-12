using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.DAL.Models;
using ProyectoFinal.DAL.Repositories.Contracts;

namespace ProyectoFinal.DAL.Repositories.Implementations
{
    public class RepositoryAuth<T> : IRepositoryAuth<T> where T : BaseAuth
    {
        private readonly DataBaseContext _context;

        public RepositoryAuth(DataBaseContext context)
        {
            _context = context;
        }
        
        public async Task<T> Login(T entity)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(g =>
                (g.Email == entity.Email || g.Login == entity.Login) && 
                g.Password == entity.Password && (bool) g.Activo);
        }

        public async Task<int> CheckExistence(T entity)
        {
            var emailExiste = await _context.Set<T>().AnyAsync(g => g.Email == entity.Email);
            var loginExiste = await _context.Set<T>().AnyAsync(g => g.Login == entity.Login);

            return emailExiste switch
            {
                true when loginExiste => 3,
                true => 1,
                _ => loginExiste ? 2 : 0
            };
        }
    }
}