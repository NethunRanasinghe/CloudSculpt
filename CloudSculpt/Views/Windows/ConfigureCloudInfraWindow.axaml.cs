using Avalonia.Controls;
using Avalonia.Interactivity;
using CloudSculpt.ViewModels;
using CloudSculpt.Views;

namespace CloudSculpt;

public partial class ConfigureCloudInfraWindow : Window
{
    public ConfigureCloudInfraWindow()
    {
        InitializeComponent();
        DataContext = new ConfigureCloudInfraViewModel();
    }

    #region Navigation

    private void ConfigCloudBackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        MainMenuWindow mainMenuWindow = new MainMenuWindow();
        Hide();
        mainMenuWindow.CurrentUserControl = new ProjectSelectionCloudView(mainMenuWindow);
        mainMenuWindow.Show();
    }

    #endregion
}