using CashApp.UI.WPF.Data.Lookups;
using CashApp.UI.WPF.Event;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

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
            _eventAggregtor.GetEvent<AfterSavedEvent>()
                .Subscribe(AfterSaveEvent);
            _eventAggregtor.GetEvent<AfterDeletedEvent>()
                .Subscribe(AfterDeleted);

        }
        private void AfterDeleted(AfterDeletedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(BalanceSheetItemDetailViewModel):
                    var balSheet = BalanceSheets.Where(item => item.Id == args.Id);
                    if (balSheet != null)
                        BalanceSheets.Remove(balSheet.SingleOrDefault());
                    break;
            }
        }
        private void AfterSaveEvent(AfterSavedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(BalanceSheetItemDetailViewModel):
                    NewNavigationItemModel(args);
                    break;
            }
        }

        private void NewNavigationItemModel(AfterSavedEventArgs args)
        {
            var SelectedItem = BalanceSheets.SingleOrDefault(bs => bs.Id == args.Id);
            if (SelectedItem == null)
            {
                BalanceSheets.Add(new BalanceSheetNavigationItemViewModel(args.Id,
                    args.Date.ToString(),
                    _eventAggregtor,
                    nameof(BalanceSheetItemDetailViewModel)));
            }
            else
            {
                SelectedItem.DisplayMember = args.Date;
            }
        }

        public async Task LoadAsync()
        {
            var lookup = await _bsLookupItem.GetBalanceSheetsAsync();
            BalanceSheets.Clear();
            foreach (var bs in lookup)
            {
                BalanceSheets.Add(new BalanceSheetNavigationItemViewModel(bs.Id,
                    bs.DisplayMember.ToShortDateString(),
                    _eventAggregtor,
                    nameof(BalanceSheetItemDetailViewModel)));
            }
        }
        public ObservableCollection<BalanceSheetNavigationItemViewModel> BalanceSheets { get; }
    }
}
