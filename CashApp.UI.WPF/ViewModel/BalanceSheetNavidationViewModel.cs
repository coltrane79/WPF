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
        private IZReadsLookup _zReadsLookupItem;
        private IEventAggregator _eventAggregtor;
        public BalanceSheetNavidationViewModel(IBalanceSheetLookupItem bsLookupItem,
            IZReadsLookup zReadsLookupItem,
            IEventAggregator eventAggregator)
        {
            _bsLookupItem = bsLookupItem;
            _zReadsLookupItem = zReadsLookupItem;
            _eventAggregtor = eventAggregator;
            BalanceSheets = new ObservableCollection<BalanceSheetNavigationItemViewModel>();
            ZReads = new ObservableCollection<BalanceSheetNavigationItemViewModel>();
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
                case nameof(ZReadDetailViewModel):
                    NewZReadNavigationItem(args);
                    break;
                default:
                    throw new Exception("View Event not Registered");
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

        private void NewZReadNavigationItem(AfterSavedEventArgs args)
        {
            var SelectedItem = ZReads.SingleOrDefault(z => z.Id == args.Id);
            if (SelectedItem == null)
            {
                ZReads.Add(new BalanceSheetNavigationItemViewModel(args.Id,
                    args.Date.ToString(),
                    _eventAggregtor,
                    nameof(ZReadDetailViewModel)));
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

            var items = await _zReadsLookupItem.GetZReadsAsyncLookup();
            ZReads.Clear();
            foreach (var item in items)
            {
                ZReads.Add(new BalanceSheetNavigationItemViewModel(item.Id,
                    item.DisplayMember.ToShortDateString(),
                    _eventAggregtor,
                    nameof(ZReadDetailViewModel)));
            }
        }
        public ObservableCollection<BalanceSheetNavigationItemViewModel> BalanceSheets { get; }
        public ObservableCollection<BalanceSheetNavigationItemViewModel> ZReads { get; }
    }
}
