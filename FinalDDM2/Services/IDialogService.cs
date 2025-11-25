namespace FinalDDM2.Services;

public interface IDialogService
{
    Task DisplayAlert(string title, string message, string cancel);
    Task<bool> DisplayConfirm(string title, string message, string accept, string cancel);
}