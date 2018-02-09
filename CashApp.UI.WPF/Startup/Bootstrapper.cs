using Autofac;
using CashApp.Data;
using CashApp.UI.WPF.Data.Lookups;
using CashApp.UI.WPF.Data.Repositories;
using CashApp.UI.WPF.ViewModel;
using CashApp.UI.WPF.Views.Services;
using Prism.Events;

namespace CashApp.UI.WPF.Startup
{
    public class Bootstrapper
    {       
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CashAppDbContext>().AsSelf();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();

            builder.RegisterType<MainViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<BalanceSheetItemDetailViewModel>().AsSelf();
            builder.RegisterType<BalanceSheetNavidationViewModel>().AsSelf();
            builder.RegisterType<LookupItem>().AsImplementedInterfaces();
            builder.RegisterType<BalanceSheetRepository>().AsImplementedInterfaces();
            builder.RegisterType<BalanceSheetNavigationItemViewModel>().AsSelf();
          
            return builder.Build();
        }
    }
}
