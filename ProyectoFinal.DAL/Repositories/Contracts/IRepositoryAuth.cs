using System.Threading.Tasks;

namespace ProyectoFinal.DAL.Repositories.Contracts
{
    public interface IRepositoryAuth<T> where T : class
    {
        Task<T> Login(T entity);
        Task<int> CheckExistence(T entity);
    }
}