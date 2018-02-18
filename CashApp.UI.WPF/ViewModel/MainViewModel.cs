using CashApp.UI.WPF.Event;
using CashApp.UI.WPF.Views.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CashApp.UI.WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {        
        private BalanceSheetNavidationViewModel _balanceSheetNavigationViewModel;
        private Func<BalanceSheetItemDetailViewModel> _balanceSheetItemDetailViewModelCreator;
        private IEventAggregator _eventAggregator { get; }
        private IMessageDialogService _messageDialogService;
        private IItemDetailViewModel _itemDetailViewModel;
        public MainViewModel(BalanceSheetNavidationViewModel BSNavigationViewModel, 
            Func<BalanceSheetItemDetailViewModel> BSItemDetailViewCreator, 
            IEventAggregator EventAggregator,
            IMessageDialogService messageDialogService)
        {
            _balanceSheetNavigationViewModel = BSNavigationViewModel;
            _balanceSheetItemDetailViewModelCreator = BSItemDetailViewCreator;
            _eventAggregator = EventAggregator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenDetailEvent>()
                .Subscribe(OnOpenDetail);
            _eventAggregator.GetEvent<AfterBalanceSheetDeletedEvent>()
                .Subscribe(OnBalanceSheetDeleted);

            OnCreateNewBalanceSheet = new DelegateCommand(CreateNewBalanceSheet);
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

        public IItemDetailViewModel ItemDetailViewModel
        {
            get { return _itemDetailViewModel; }
            private set
            {
                _itemDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        { 
            await _balanceSheetNavigationViewModel.LoadAsync();
        }
        public ICommand OnCreateNewBalanceSheet { get; private set; }

        private void CreateNewBalanceSheet()
        {
            OnOpenDetail(null);
        }
        private async void OnOpenDetail(OpenDetailEventEventArgs args)
        {
            if(ItemDetailViewModel != null && ItemDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("Leave Un-saved Data?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            switch (args.ViewModelName)
            {
                case nameof(BalanceSheetItemDetailViewModel):
                    ItemDetailViewModel = _balanceSheetItemDetailViewModelCreator();
                    break;
            }
            
            await ItemDetailViewModel.LoadAsync(args.id);
        }

        private void OnBalanceSheetDeleted(BalanceSheetSavedEventArgs obj)
        {
            ItemDetailViewModel = null;
        }

    }
}
