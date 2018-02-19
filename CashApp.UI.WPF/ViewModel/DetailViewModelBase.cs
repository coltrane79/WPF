using CashApp.UI.WPF.Event;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;

namespace CashApp.UI.WPF.ViewModel
{
    public abstract class DetailViewModelBase : ViewModelBase, IItemDetailViewModel
    {
        protected readonly IEventAggregator EventAggregator;
        private bool _hasChanges;

        public DetailViewModelBase(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
        }

        public abstract Task LoadAsync(int? Id);
       
        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if(_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        protected abstract void OnDeleteExecute();
        protected abstract bool OnSaveCanExecute();
        protected abstract void OnSaveExecute();        

        protected virtual void RaiseDetailDeletedEvent(int modelId)
        {
            EventAggregator.GetEvent<AfterDeletedEvent>()
                .Publish(
                new AfterDeletedEventArgs
                {
                    Id = modelId,
                    ViewModelName = this.GetType().Name
                });
        }

        protected virtual void RaiseDetailSavedEvent(int modelId, string displayMember)
        {
            EventAggregator.GetEvent<AfterSavedEvent>()
                .Publish(
                new AfterSavedEventArgs
                {
                    Id = modelId,
                    Date = displayMember,
                    ViewModelName = this.GetType().Name
                });
        }                
    }
}
