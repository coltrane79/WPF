using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashApp.UI.WPF.Event
{
    public class AfterBalanceSheetSavedEvent: PubSubEvent<BalanceSheetSavedEventArgs>
    {
    }

    public class BalanceSheetSavedEventArgs
    {
        public int Id { get; set; }
        public string Date { get; set; }
    }
}
