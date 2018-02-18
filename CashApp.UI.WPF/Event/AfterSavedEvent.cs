using Prism.Events;

namespace CashApp.UI.WPF.Event
{
    public class AfterSavedEvent: PubSubEvent<AfterSavedEventArgs>
    {
    }

    public class AfterSavedEventArgs
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string ViewModelName { get; set; }
    }
}
