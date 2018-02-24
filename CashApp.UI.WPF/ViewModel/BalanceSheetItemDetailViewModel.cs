using CashApp.Model.Model;
using CashApp.UI.WPF.Data.Lookups;
using CashApp.UI.WPF.Data.Repositories;
using CashApp.UI.WPF.ModelWrapper;
using CashApp.UI.WPF.Views.Services;
using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace CashApp.UI.WPF.ViewModel
{
    public class BalanceSheetItemDetailViewModel : DetailViewModelBase, IBalanceSheetItemDetailViewModel
    {

        private BalanceSheetModelWrapper _cashBalanceSheet;
        private IBalanceSheetRespository _balanceSheetRepository;
        private ISalesPersonLookupItem _salesPersonLookupItem;
        private IZReadsLookup _zReadsLookup;
        private IZReadRepository _zReadRepository;
        private ZReadModelWrapper _selectedZRead;      

        public BalanceSheetItemDetailViewModel(IBalanceSheetRespository BalanceSheetRepository,
            IEventAggregator eventAggregator,
            ISalesPersonLookupItem salesPersonLookupItem,
            IZReadsLookup zReadsLookup,
            IZReadRepository ZReadRepository,
            IMessageDialogService messageDialogService) : base(eventAggregator, messageDialogService)
        {
            _balanceSheetRepository = BalanceSheetRepository;
            _salesPersonLookupItem = salesPersonLookupItem;
            _zReadsLookup = zReadsLookup;
            _zReadRepository = ZReadRepository;

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
            if (_selectedZRead != null)
            {
                _zReadRepository.DeletebyIdAsync(_selectedZRead.Model);
                await _zReadRepository.SaveAsync();
                await LoadZReads();
            }
        }
        private async void OnAddZRead()
        {
            ZRead zread = new ZRead(CashBalanceSheetProperty.Date);
            _zReadRepository.Add(zread);
            await _zReadRepository.SaveAsync();
            ZReads.Add(new ZReadModelWrapper(zread));
            //ZReadModelWrapper zreadmodelwrapper = new ZReadModelWrapper()
        }
        protected override bool OnSaveCanExecute()
        {
            return CashBalanceSheetProperty != null
                && !CashBalanceSheetProperty.HasErrors
                && HasChanges;
        }
        protected override async void OnSaveExecute()
        {
            try
            {
                await _balanceSheetRepository.SaveAsync();
                HasChanges = _balanceSheetRepository.HasChanges();
                RaiseDetailSavedEvent(CashBalanceSheetProperty.Id, CashBalanceSheetProperty.Date.ToShortDateString());
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var result = _messageDialogService.ShowOkCancelDialog("An Update has Occured after you have loaded the record",
                    "Continue?");
                if(result == MessageDialogResult.Ok)
                {
                    //HasChanges = _balanceSheetRepository.HasChanges();
                    //Id = CashBalanceSheetProperty.Id;
                    //RaiseDetailSavedEvent(CashBalanceSheetProperty.Id, CashBalanceSheetProperty.Date.ToShortDateString());
                    var entry = ex.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    await _balanceSheetRepository.SaveAsync();
                    HasChanges = _balanceSheetRepository.HasChanges();
                    RaiseDetailSavedEvent(CashBalanceSheetProperty.Id, CashBalanceSheetProperty.Date.ToShortDateString());
                }
                else
                {
                    await ex.Entries.Single().ReloadAsync();
                    await LoadAsync(CashBalanceSheetProperty.Id);
                    HasChanges = _balanceSheetRepository.HasChanges();
                }
               
            }
            
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
        public ICommand AddZread { get; set; }
        public ICommand DeleteZread { get; set; }
        public ObservableCollection<SalesPersonLookupItem> SalesPeople { get; }
        public ObservableCollection<ZReadModelWrapper> ZReads { get; private set; }
        public override async Task LoadAsync(int? Id)
        {
            var newBalanceSheet = Id.HasValue
                ? await _balanceSheetRepository.GetByIdAsync(Id.Value)
                : await CreateNewBalanceSheet();

            InitializeBalanceSheet(newBalanceSheet);
            
        }

        private async void InitializeBalanceSheet(CashBalanceSheet balanceSheet)
        {
            CashBalanceSheetProperty = new BalanceSheetModelWrapper(balanceSheet);
            Id = CashBalanceSheetProperty.Id;
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
                if (e.PropertyName == nameof(CashBalanceSheet.Date))
                {
                    SetTitle();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            SetTitle();

            await LoadSalesPeople();

            await LoadZReads();
        }

        private void SetTitle()
        {
            Title = CashBalanceSheetProperty.Date.ToShortDateString();
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
        private async Task<CashBalanceSheet> CreateNewBalanceSheet()
        {
            var balanceSheet = new CashBalanceSheet();
            _balanceSheetRepository.Add(balanceSheet);
            await _balanceSheetRepository.SaveAsync();
            return balanceSheet;
        }
        private bool OnDeleteCanExecute()
        {
            return true;
        }
        protected override async void OnDeleteExecute()
        {
            var result = new MessageDialogService().
                ShowOkCancelDialog("Do you really want to Delete?", "Delete Balance Sheet");
            if (result == MessageDialogResult.Ok)
            {
                _balanceSheetRepository.DeletebyIdAsync(CashBalanceSheetProperty.Model);
                await _balanceSheetRepository.SaveAsync();
                RaiseDetailDeletedEvent(CashBalanceSheetProperty.Id);
            }
        }
    }
}
