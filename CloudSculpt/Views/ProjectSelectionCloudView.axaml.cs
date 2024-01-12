using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace CloudSculpt.Views;

public partial class ProjectSelectionCloudView : UserControl
{
    private readonly MainMenuWindow _mainMenuWindow;
    
    public ProjectSelectionCloudView(MainMenuWindow mainMenuWindow)
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
        ProjectSelectionMainView projectSelectionMainView = new(_mainMenuWindow);
        _mainMenuWindow.CurrentUserControl = projectSelectionMainView;
    }
    #endregion
}