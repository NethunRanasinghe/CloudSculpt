using Avalonia.Controls;
using Avalonia.Interactivity;
using CloudSculpt.ViewModels;

namespace CloudSculpt;

public partial class SettingsWindow : Window
{
    public SettingsWindow()
    {
        InitializeComponent();
        DataContext = new SettingsViewModel();
    }

    #region Navigation
    private void SettingsBackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Views.Windows.MainMenuWindow mainMenuWindow = new();
        this.Hide();
        mainMenuWindow.Show();
    }
    #endregion
}