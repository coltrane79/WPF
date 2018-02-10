using Prism.Events;

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
