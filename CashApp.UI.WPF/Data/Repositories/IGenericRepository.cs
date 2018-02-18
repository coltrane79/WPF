using System.Threading.Tasks;

namespace CashApp.UI.WPF.Data.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task SaveAsync();
        bool HasChanges();
        void Add(T Model);
        void DeletebyIdAsync(T Model);
    }
}
