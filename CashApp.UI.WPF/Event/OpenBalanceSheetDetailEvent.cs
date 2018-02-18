using Prism.Events;

namespace CashApp.UI.WPF.Event
{
    public class OpenDetailEvent: PubSubEvent<OpenDetailEventEventArgs>
    {
    }    
    public class OpenDetailEventEventArgs
    {
        public int? id { get; set; }
        public string ViewModelName { get; set; }
    }
}
