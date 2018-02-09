using System.Threading.Tasks;

namespace CashApp.UI.WPF.ViewModel
{
    public interface IBalanceSheetItemDetailViewModel
    {
        Task LoadAsync(int Id);
        bool HasChanges { get; }
    }
}