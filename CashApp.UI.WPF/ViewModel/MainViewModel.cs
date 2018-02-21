using Autofac.Features.Indexed;
using CashApp.UI.WPF.Event;
using CashApp.UI.WPF.Views.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private IItemDetailViewModel _selectedItemDetailViewModel;
        public MainViewModel(BalanceSheetNavidationViewModel BSNavigationViewModel, 
            IIndex<string, IItemDetailViewModel> DetailViewModelCreator,
            IEventAggregator EventAggregator,
            IMessageDialogService messageDialogService)
        {
            _balanceSheetNavigationViewModel = BSNavigationViewModel;
            _detailViewModelCreator = DetailViewModelCreator;
            _eventAggregator = EventAggregator;
            _messageDialogService = messageDialogService;
            DetailViewModels = new ObservableCollection<IItemDetailViewModel>();

            _eventAggregator.GetEvent<OpenDetailEvent>()
                .Subscribe(OnOpenDetail);
            _eventAggregator.GetEvent<AfterDeletedEvent>()
                .Subscribe(OnDeleted);
            _eventAggregator.GetEvent<AfterDetailClosedEvent>()
                .Subscribe(OnDeleteViewClose);


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

        public ObservableCollection<IItemDetailViewModel> DetailViewModels { get; }
        public IItemDetailViewModel SelectedDetailViewModel
        {
            get { return _selectedItemDetailViewModel; }
            set
            {
                _selectedItemDetailViewModel = value;
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
            var detailViewModel = DetailViewModels.
                SingleOrDefault(vm => vm.Id == args.id
                && vm.GetType().Name == args.ViewModelName);
            if(detailViewModel == null  && !DetailViewModels.Contains(detailViewModel))
            {
                detailViewModel = _detailViewModelCreator[args.ViewModelName];
                await detailViewModel.LoadAsync(args.id);
                DetailViewModels.Add(detailViewModel);
            }

            SelectedDetailViewModel = detailViewModel;

            await SelectedDetailViewModel.LoadAsync(args.id);
        }

        private void OnDeleted(AfterDeletedEventArgs args)
        {
            DeleteDetailView(args.Id, args.ViewModelName);
        }

        private void DeleteDetailView(int Id, string ViewModelName)
        {
            var detailViewModel = DetailViewModels.
                             SingleOrDefault(vm => vm.Id == Id
                             && vm.GetType().Name == ViewModelName);

            if (detailViewModel != null)
            {
                DetailViewModels.Remove(detailViewModel);
            }
        }

        private void OnDeleteViewClose(AfterDetailCloseEventArgs args)
        {
            DeleteDetailView(args.Id, args.ViewModelName);
        }

    }
}
