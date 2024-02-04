using Avalonia.Controls;
using Avalonia.Interactivity;
using CloudSculpt.HelperClasses;

namespace CloudSculpt.Views.UserControls;

public partial class ProjectSelectionNetworkView : UserControl
{
    private readonly Windows.MainMenuWindow _mainMenuWindow;

    public ProjectSelectionNetworkView(Windows.MainMenuWindow mainMenuWindow)
    {
        InitializeComponent();
        _mainMenuWindow = mainMenuWindow;
    }
    
    #region Navigation
    
    private void NetworkCloudButton_OnClick(object? sender, RoutedEventArgs e)
    {
        UserControls.ProjectSelectionCloudView projectSelectionCloudView = new(_mainMenuWindow);
        _mainMenuWindow.CurrentUserControl = projectSelectionCloudView;
    }
    
    private void NetworkBackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        ApplicationControl.ToMainMenuViewFromMainMenuWindow(_mainMenuWindow);
    }

    #endregion

}