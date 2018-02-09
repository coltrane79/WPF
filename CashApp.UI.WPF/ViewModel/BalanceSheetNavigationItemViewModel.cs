using CashApp.UI.WPF.Event;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CashApp.UI.WPF.ViewModel
{
    public class BalanceSheetNavigationItemViewModel: ViewModelBase
    {
        public int Id { get; set; }
        private string _displayMember;
        private IEventAggregator _eventAggregator;
        public BalanceSheetNavigationItemViewModel(int id, string DisplayMember,
            IEventAggregator EventAggregator )
        {
            Id = id;
            _displayMember = DisplayMember;
            OnOpenBalanceSheetDetailCommand = new DelegateCommand(OnOpenBalanceSheetViewCommand);
            _eventAggregator = EventAggregator;
        }

        
        public string DisplayMember
        {
            get { return "Bal. Sheet Date: " + _displayMember; }
            set
            {
                _displayMember = value;
                OnPropertyChanged(nameof(DisplayMember));
            }
        }

        public ICommand OnOpenBalanceSheetDetailCommand { get; }
        
        private void OnOpenBalanceSheetViewCommand()
        {
            _eventAggregator.GetEvent<OpenBalanceSheetDetailEvent>()
                        .Publish(Id);
        }

    }
}
