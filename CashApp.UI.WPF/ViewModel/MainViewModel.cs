using Autofac.Features.Indexed;
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
        private IIndex<string, IItemDetailViewModel> _detailViewModelCreator;        
        private IEventAggregator _eventAggregator { get; }
        private IMessageDialogService _messageDialogService;
        private IItemDetailViewModel _itemDetailViewModel;
        public MainViewModel(BalanceSheetNavidationViewModel BSNavigationViewModel, 
            IIndex<string, IItemDetailViewModel> DetailViewModelCreator,
            IEventAggregator EventAggregator,
            IMessageDialogService messageDialogService)
        {
            _balanceSheetNavigationViewModel = BSNavigationViewModel;
            _detailViewModelCreator = DetailViewModelCreator;
            _eventAggregator = EventAggregator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenDetailEvent>()
                .Subscribe(OnOpenDetail);
            _eventAggregator.GetEvent<AfterDeletedEvent>()
                .Subscribe(OnDeleted);

            OnCreateNewBalanceSheet = new DelegateCommand<Type>(CreateNewBalanceSheet);
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

        private void CreateNewBalanceSheet(Type viewModelName)
        {
            OnOpenDetail(new OpenDetailEventEventArgs { ViewModelName = viewModelName.Name });
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

            ItemDetailViewModel = _detailViewModelCreator[args.ViewModelName];

            await ItemDetailViewModel.LoadAsync(args.id);
        }

        private void OnDeleted(AfterDeletedEventArgs args)
        {
            ItemDetailViewModel = null;
        }

    }
}
