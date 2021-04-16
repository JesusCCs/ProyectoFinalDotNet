using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.DAL.Models;
using ProyectoFinal.DAL.Models.Auth;
using ProyectoFinal.DAL.Repositories.Contracts;

namespace ProyectoFinal.DAL.Repositories.Implementations
{
    public class RepositoryAuth<T> : IRepositoryAuth<T> where T : Auth
    {
        private readonly DataBaseContext _context;

        public RepositoryAuth(DataBaseContext context)
        {
            _context = context;
        }
        
        public async Task<T> Login(T entity)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(g =>
                (g.Email == entity.Email || g.UserName == entity.UserName) && 
                g.PasswordHash == entity.PasswordHash && g.EmailConfirmed);
        }

        public async Task<int> CheckExistence(T entity)
        {
            var emailExiste = await _context.Set<T>().AnyAsync(g => g.Email == entity.Email);
            var loginExiste = await _context.Set<T>().AnyAsync(g => g.UserName == entity.UserName);

            return emailExiste switch
            {
                true when loginExiste => 3,
                true => 1,
                _ => loginExiste ? 2 : 0
            };
        }
    }
}