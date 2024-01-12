using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace CloudSculpt.Views;

public partial class ProjectSelectionNetworkView : UserControl
{
    private readonly MainMenuWindow _mainMenuWindow;

    public ProjectSelectionNetworkView(MainMenuWindow mainMenuWindow)
    {
        InitializeComponent();
        _mainMenuWindow = mainMenuWindow;
    }
    
    #region Navigation
    
    private void NetworkCloudButton_OnClick(object? sender, RoutedEventArgs e)
    {
        ProjectSelectionCloudView projectSelectionCloudView = new(_mainMenuWindow);
        _mainMenuWindow.CurrentUserControl = projectSelectionCloudView;
    }
    
    private void NetworkBackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        ProjectSelectionMainView projectSelectionMainView = new(_mainMenuWindow);
        _mainMenuWindow.CurrentUserControl = projectSelectionMainView;
    }

    #endregion

}