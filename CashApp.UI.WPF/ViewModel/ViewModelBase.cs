using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CashApp.UI.WPF.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            var handler = PropertyChanged;
            if(PropertyChanged != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
        }

        
    }
}
