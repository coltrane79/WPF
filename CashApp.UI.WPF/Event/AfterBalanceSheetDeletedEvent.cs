using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashApp.UI.WPF.Event
{
    public class AfterBalanceSheetDeletedEvent: PubSubEvent<BalanceSheetSavedEventArgs>
    {
    }
}
