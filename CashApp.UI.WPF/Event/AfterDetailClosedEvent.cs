using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashApp.UI.WPF.Event
{
    class AfterDetailClosedEvent: PubSubEvent<AfterDetailCloseEventArgs>
    {
    }

    public class AfterDetailCloseEventArgs
    {
        public int Id { get; set; }
        public string ViewModelName { get; set; }
    }
}
