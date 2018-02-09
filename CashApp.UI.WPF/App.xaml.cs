using Autofac;
using CashApp.UI.WPF.Startup;
using System.Windows;

namespace CashApp.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
       public void Application_Startup(object sender, StartupEventArgs e)
       {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Bootstrap();
            var mainWindow = container.Resolve<MainWindow>();

            mainWindow.Show();
       }

        private void Application_DispatcherUnhandledException(object sender, 
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Error Occurred; Error Details: /n"+ e.Exception.Message);
            e.Handled = true;
        }
    }
}
