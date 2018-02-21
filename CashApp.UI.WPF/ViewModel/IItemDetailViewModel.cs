using System.Threading.Tasks;

namespace CashApp.UI.WPF.ViewModel
{
    public interface IItemDetailViewModel
    {
        Task LoadAsync(int? Id);
        bool HasChanges { get; }
        int Id { get; }
    }
}