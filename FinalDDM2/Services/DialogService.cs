namespace FinalDDM2.Services;

public class DialogService : IDialogService
{
    public Task DisplayAlert(string title, string message, string cancel)
    {
        return MainThread.InvokeOnMainThreadAsync(async () => 
        {
            await Shell.Current.DisplayAlert(title, message, cancel);
        });
    }

    public Task<bool> DisplayConfirm(string title, string message, string accept, string cancel)
    {
        return MainThread.InvokeOnMainThreadAsync(async () => 
        {
            return await Shell.Current.DisplayAlert(title, message, accept, cancel);
        });
    }
}