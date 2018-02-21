using CashApp.Model.Model;
using CashApp.UI.WPF.Data.Repositories;
using CashApp.UI.WPF.ModelWrapper;
using CashApp.UI.WPF.Views.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashApp.UI.WPF.ViewModel
{
    public class ZReadDetailViewModel : DetailViewModelBase, IZReadViewModel
    {
        private IMessageDialogService _messageDialog;
        private ZReadModelWrapper _ZRead;
        private IZReadRepository _zReadRepository;

        public ZReadDetailViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDialog,
            IZReadRepository zReadRepository)
            : base(eventAggregator)
        {
            _messageDialog = messageDialog;
            _zReadRepository = zReadRepository;
        }

        public ZReadModelWrapper ZRead
        {
            get { return _ZRead; }
            private set
            {
                _ZRead = value;
                OnPropertyChanged();
            }
        }

        public async override Task LoadAsync(int? Id)
        {
            var zread = Id.HasValue
                ? await _zReadRepository.GetByIdAsync(Id)
                : CreateNewZRead();

            //Id = zread.Id;

            InitializeZRead((ZRead)zread);
        }

        private void InitializeZRead(ZRead zread)
        {
            ZRead = new ZReadModelWrapper(zread);
            Id = ZRead.Id;
            ZRead.PropertyChanged += (s, e) =>
                {
                    if (!HasChanges)
                    {
                        HasChanges = _zReadRepository.HasChanges();
                    }

                    if (e.PropertyName == nameof(ZRead.HasErrors))
                    {
                        ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                    }
                    if(e.PropertyName == nameof(ZRead.ZReadDate))
                    {
                        SetTitle();
                    }
                };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            SetTitle();
        }

        private void SetTitle()
        {
            Title = ZRead.ZReadDate.ToShortDateString();
        }

        private ZRead CreateNewZRead()
        {
            var zread = new ZRead(DateTime.Today);
            _zReadRepository.Add(zread);
            return zread;
        }

        protected override void OnDeleteExecute()
        {
            if (HasChanges)
            {
                MessageDialogResult message  = _messageDialog.ShowOkCancelDialog(
                    "Navigate Away from Unsaved Data", "Leave");
                if (message == MessageDialogResult.Ok)
                {
                    _zReadRepository.DeletebyIdAsync(ZRead.Model);
                    _zReadRepository.SaveAsync();
                    RaiseDetailDeletedEvent(ZRead.Id);
                }                                    
            }
        }
        protected override bool OnSaveCanExecute()
        {
            return ZRead != null 
                && !ZRead.HasErrors 
                && HasChanges;
        }

        protected async override void OnSaveExecute()
        {
            await _zReadRepository.SaveAsync();
            HasChanges = _zReadRepository.HasChanges();
            Id = ZRead.Id;
            RaiseDetailSavedEvent(ZRead.Model.Id, ZRead.Model.ZReadDate.ToShortDateString());
        }
    }
}
