using CashApp.UI.WPF.ViewModel;
using System.Windows;

namespace CashApp.UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///    

    public partial class MainWindow : Window
    {

        private MainViewModel _viewModel;
        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            _viewModel = mainViewModel;
            DataContext = _viewModel;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }
    }
}
