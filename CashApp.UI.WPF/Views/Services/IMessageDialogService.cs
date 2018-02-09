namespace CashApp.UI.WPF.Views.Services
{
    public interface IMessageDialogService
    {
        MessageDialogResult ShowOkCancelDialog(string message, string title);
    }
}