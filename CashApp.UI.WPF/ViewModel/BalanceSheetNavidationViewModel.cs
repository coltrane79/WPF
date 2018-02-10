using CashApp.UI.WPF.Data.Lookups;
using CashApp.UI.WPF.Event;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CashApp.UI.WPF.ViewModel
{
    public class BalanceSheetNavidationViewModel : ViewModelBase
    {
        private IBalanceSheetLookupItem _bsLookupItem;
        private IEventAggregator _eventAggregtor; 
        public BalanceSheetNavidationViewModel(IBalanceSheetLookupItem bsLookupItem,
            IEventAggregator eventAggregator)
        {
            _bsLookupItem = bsLookupItem;
            _eventAggregtor = eventAggregator;
            BalanceSheets = new ObservableCollection<BalanceSheetNavigationItemViewModel>();
            _eventAggregtor.GetEvent<AfterBalanceSheetSavedEvent>()
                .Subscribe(AfterFriendSaveEvent);
        }

        private void AfterFriendSaveEvent(BalanceSheetSavedEventArgs updatedBalanceSheet)
        {
            var SelectedItem = BalanceSheets.SingleOrDefault(bs => bs.Id == updatedBalanceSheet.Id);
            if(SelectedItem == null)
            {
                BalanceSheets.Add(new BalanceSheetNavigationItemViewModel(updatedBalanceSheet.Id,
                    updatedBalanceSheet.Date.ToString(), _eventAggregtor));
            } else
            {
                SelectedItem.DisplayMember = updatedBalanceSheet.Date;
            }            
        }

        public async Task LoadAsync()
        {
            var lookup = await _bsLookupItem.GetBalanceSheetsAsync();
            BalanceSheets.Clear();
            foreach (var bs in lookup)
            {
                BalanceSheets.Add(new BalanceSheetNavigationItemViewModel(bs.Id, bs.DisplayMember.ToShortDateString(),
                    _eventAggregtor));                
            }
        }
        public ObservableCollection<BalanceSheetNavigationItemViewModel> BalanceSheets { get; }
       
    }
}
