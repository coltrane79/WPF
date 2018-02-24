using CashApp.UI.WPF.Event;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;
using System;
using CashApp.UI.WPF.Views.Services;

namespace CashApp.UI.WPF.ViewModel
{
    public abstract class DetailViewModelBase : ViewModelBase, IItemDetailViewModel
    {
        protected readonly IEventAggregator EventAggregator;
        protected IMessageDialogService _messageDialogService;
        private bool _hasChanges;
        private int _id;

        public DetailViewModelBase(IEventAggregator eventAggregator, IMessageDialogService MessageDialogService)
        {
            EventAggregator = eventAggregator;
            _messageDialogService = MessageDialogService;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
            DetailClose = new DelegateCommand(OnDetailClose);
        }

        private void OnDetailClose()
        {
            if (HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("Leave Un-Saved Data?", "Question");
                if(result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            EventAggregator.GetEvent<AfterDetailClosedEvent>()
                .Publish(new AfterDetailCloseEventArgs
                {
                    Id = this._id,
                    ViewModelName = this.GetType().Name
                });
        }

        public abstract Task LoadAsync(int? Id);

        public DelegateCommand DetailClose { get; set; }
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

        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }


        public int Id
        {
            get { return _id; }
            protected set { _id = value; }
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
