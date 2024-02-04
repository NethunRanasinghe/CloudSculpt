using Avalonia.Controls;
using Avalonia.Interactivity;
using CloudSculpt.HelperClasses;

namespace CloudSculpt.Views.UserControls;

public partial class ProjectSelectionCloudView : UserControl
{
    private readonly Windows.MainMenuWindow _mainMenuWindow;
    
    public ProjectSelectionCloudView(Windows.MainMenuWindow mainMenuWindow)
    {
        InitializeComponent();
        _mainMenuWindow = mainMenuWindow;
    }

    #region Navigation
    private void CloudNetworkButton_OnClick(object? sender, RoutedEventArgs e)
    {
        ProjectSelectionNetworkView projectSelectionNetworkView = new(_mainMenuWindow);
        _mainMenuWindow.CurrentUserControl = projectSelectionNetworkView;
    }
    private void CloudBackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        ApplicationControl.ToMainMenuViewFromMainMenuWindow(_mainMenuWindow);
    }
    
    private void CloudConfigCloudButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Windows.ConfigureCloudInfraWindow configureCloudInfraWindow = new();
        _mainMenuWindow.Hide();
        configureCloudInfraWindow.Show();
    }
    #endregion
}