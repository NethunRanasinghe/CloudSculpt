using Avalonia.Controls;

namespace CloudSculpt.Interfaces;

public interface INavigationService
{
    void NavigateTo(string windowKey);
    void NavigateAndChangeUserControl(string windowKey, UserControl userControl);
}