using CashApp.Model.Model;
using CashApp.UI.WPF.Data.Repositories;
using CashApp.UI.WPF.Event;
using CashApp.UI.WPF.ModelWrapper;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using CashApp.UI.WPF.Views.Services;
using CashApp.UI.WPF.Data.Lookups;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace CashApp.UI.WPF.ViewModel
{
    public class BalanceSheetItemDetailViewModel : ViewModelBase, IBalanceSheetItemDetailViewModel
    {
        private BalanceSheetModelWrapper _cashBalanceSheet;
        private IBalanceSheetRespository _balanceSheetRepository;        
        private IEventAggregator _eventAggregator;
        private ISalesPersonLookupItem _salesPersonLookupItem;
        private IZReadsLookup _zReadsLookup;
        private IZReadRepository _zReadRepository;
        private bool _hasChanges;
        private ZReadModelWrapper _selectedZRead;

        public BalanceSheetItemDetailViewModel(IBalanceSheetRespository BalanceSheetRepository,
            IEventAggregator eventAggregator,
            ISalesPersonLookupItem salesPersonLookupItem,
            IZReadsLookup zReadsLookup,
            IZReadRepository ZReadRepository)
        {
            _balanceSheetRepository = BalanceSheetRepository;
            _eventAggregator = eventAggregator;
            _salesPersonLookupItem = salesPersonLookupItem;
            _zReadsLookup = zReadsLookup;
            _zReadRepository = ZReadRepository;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute, OnDeleteCanExecute);
            AddZread = new DelegateCommand(OnAddZRead);
            DeleteZread = new DelegateCommand(OnDeleteZRead, OnDeleteZReadCanExecute);

            SalesPeople = new ObservableCollection<SalesPersonLookupItem>();
            ZReads = new ObservableCollection<ZReadModelWrapper>();
        }

        private bool OnDeleteZReadCanExecute()
        {
            return SelectedZRead != null;
        }

        private async void OnDeleteZRead()
        {
           if(_selectedZRead != null)
           {
                _zReadRepository.Remove(_selectedZRead.Model);
                await LoadZReads();
           }
        }

        private async void OnAddZRead()
        {
            ZRead zread = new ZRead(CashBalanceSheetProperty.Date);
            _zReadRepository.Add(zread);
            await _zReadRepository.SaveChanges();
            ZReads.Add(new ZReadModelWrapper(zread));
            //ZReadModelWrapper zreadmodelwrapper = new ZReadModelWrapper()
        }

        private bool OnSaveCanExecute()
        {
            return CashBalanceSheetProperty != null && !CashBalanceSheetProperty.HasErrors && HasChanges;
        }
        private async void OnSaveExecute()
        {
            await _balanceSheetRepository.SaveAsync();
            HasChanges = _balanceSheetRepository.HasChanges();
            _eventAggregator.GetEvent<AfterBalanceSheetSavedEvent>()
                .Publish(new BalanceSheetSavedEventArgs
                {
                    Id = CashBalanceSheetProperty.Id,
                    Date = CashBalanceSheetProperty.Date.ToShortDateString()
                }
                );
        }
        public BalanceSheetModelWrapper CashBalanceSheetProperty
        {
            get { return _cashBalanceSheet; }
            set
            {
                _cashBalanceSheet = value;
                OnPropertyChanged();
            }
        }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand AddZread { get; set; }
        public ICommand DeleteZread { get; set; }
        public ObservableCollection<SalesPersonLookupItem> SalesPeople { get; }
        public ObservableCollection<ZReadModelWrapper> ZReads { get; private set; }
        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public async Task LoadAsync(int? Id)
        {
            var newBalanceSheet = Id.HasValue
                ? await _balanceSheetRepository.GetByIdAsync(Id.Value)
                : CreateNewBalanceSheet();

            CashBalanceSheetProperty = new BalanceSheetModelWrapper(newBalanceSheet);


            CashBalanceSheetProperty.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _balanceSheetRepository.HasChanges();
                }
                if (e.PropertyName == nameof(CashBalanceSheetProperty.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            await LoadSalesPeople();

            await LoadZReads();
        }

        private async Task LoadSalesPeople()
        {
            SalesPeople.Clear();
            var lookup = await _salesPersonLookupItem.GetSalesPersonAsync();
            foreach (var item in lookup)
            {
                SalesPeople.Add(item);
            }
        }

        private async Task LoadZReads()
        {
            ZReads.Clear();
            var zreads = await _zReadsLookup.GetZReadsAsync(CashBalanceSheetProperty.Date);
            foreach (var zread in zreads)
            {
                var zReadModelWrapper = new ZReadModelWrapper(zread);
                ZReads.Add(zReadModelWrapper);
            }
        }

        public ZReadModelWrapper SelectedZRead
        {
            get { return _selectedZRead; }
            set
            {
                _selectedZRead = value;
                OnPropertyChanged();
                ((DelegateCommand)DeleteZread).RaiseCanExecuteChanged();
            }
        }
        private CashBalanceSheet CreateNewBalanceSheet()
        {
            var balanceSheet = new CashBalanceSheet();
            _balanceSheetRepository.Add(balanceSheet);
            return balanceSheet;
        }
        private bool OnDeleteCanExecute()
        {
            return true;    
        }
        private async void OnDeleteExecute()
        {
            var result = new MessageDialogService().
                ShowOkCancelDialog("Do you really want to Delete?", "Delete Balance Sheet");
            if (result == MessageDialogResult.Ok)
            {
                _balanceSheetRepository.DeletebyIdAsync(CashBalanceSheetProperty.Model);
                await _balanceSheetRepository.SaveAsync();
                _eventAggregator.GetEvent<AfterBalanceSheetDeletedEvent>()
                    .Publish(new BalanceSheetSavedEventArgs()
                    {
                        Id = CashBalanceSheetProperty.Id
                    });
            }
        }

    }
}
