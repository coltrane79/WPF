using CashApp.Model.Model;
using CashApp.UI.WPF.Data.Repositories;
using CashApp.UI.WPF.Event;
using CashApp.UI.WPF.ModelWrapper;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CashApp.UI.WPF.ViewModel
{
    public class BalanceSheetItemDetailViewModel : ViewModelBase, IBalanceSheetItemDetailViewModel
    {
        private BalanceSheetModelWrapper _cashBalanceSheet;
        private IBalanceSheetRespository _balanceSheetRepository;
        private IEventAggregator _eventAggregator;
        private bool _hasChanges;

        


        public BalanceSheetItemDetailViewModel(IBalanceSheetRespository BalanceSheetRepository, 
            IEventAggregator eventAggregator)
        {
            _balanceSheetRepository = BalanceSheetRepository;
            _eventAggregator = eventAggregator;            

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
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

        public async Task LoadAsync(int Id)
        {
            CashBalanceSheet newBalanceSheet = await _balanceSheetRepository.GetByIdAsync(Id);
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
        }

    }
}
