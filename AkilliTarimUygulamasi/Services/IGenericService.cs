using AkilliTarimUygulamasi.Models;

namespace AkilliTarimUygulamasi.Services
{
    public interface IGenericService<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<bool> UpdateAsync(User updatedUser);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}