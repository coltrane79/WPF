using CashApp.UI.WPF.Event;
using CashApp.UI.WPF.Views.Services;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CashApp.UI.WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {        
        private BalanceSheetNavidationViewModel _balanceSheetNavigationViewModel;
        private Func<BalanceSheetItemDetailViewModel> _balanceSheetItemDetailViewModelCreator;
        private IEventAggregator _eventAggregator { get; }
        private IMessageDialogService _messageDialogService;
        public MainViewModel(BalanceSheetNavidationViewModel BSNavigationViewModel, Func<BalanceSheetItemDetailViewModel>
            BSItemDetailViewCreator, IEventAggregator EventAggregator,
            IMessageDialogService messageDialogService)
        {
            _balanceSheetNavigationViewModel = BSNavigationViewModel;
            _balanceSheetItemDetailViewModelCreator = BSItemDetailViewCreator;
            _eventAggregator = EventAggregator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenBalanceSheetDetailEvent>()
                .Subscribe(OnOpenBalanceSheetDetail);
        }        
        public BalanceSheetNavidationViewModel BalanceSheetNavigationViewModel
        {
            get { return _balanceSheetNavigationViewModel; }
            set
            {
                _balanceSheetNavigationViewModel = value;
                OnPropertyChanged(nameof(value));
            }
        }

        private IBalanceSheetItemDetailViewModel balanceSheetItemDetailViewModel;

        public IBalanceSheetItemDetailViewModel BalanceSheetItemDetailViewModel
        {
            get { return balanceSheetItemDetailViewModel; }
            private set
            {
                balanceSheetItemDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        { 
            await _balanceSheetNavigationViewModel.LoadAsync();
        }

        private async void OnOpenBalanceSheetDetail(int balanceSheetId)
        {
            if(BalanceSheetItemDetailViewModel != null && BalanceSheetItemDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("Leave Un-saved Data?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            BalanceSheetItemDetailViewModel = _balanceSheetItemDetailViewModelCreator();
            await BalanceSheetItemDetailViewModel.LoadAsync(balanceSheetId);
        }

    }
}
