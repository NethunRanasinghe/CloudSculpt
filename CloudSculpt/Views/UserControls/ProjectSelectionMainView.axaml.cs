using Avalonia.Controls;
using Avalonia.Interactivity;
using CloudSculpt.HelperClasses;
using CloudSculpt.Views.Windows;

namespace CloudSculpt.Views.UserControls;

public partial class ProjectSelectionMainView : UserControl
{
    private readonly Windows.MainMenuWindow _mainMenuWindow;
    
    public ProjectSelectionMainView(Windows.MainMenuWindow mainMenuWindow)
    {
        InitializeComponent();
        _mainMenuWindow = mainMenuWindow;
    }

    #region Navigation
    private void MainNewProjectButton_OnClick(object? sender, RoutedEventArgs e)
    {
        UserControls.ProjectSelectionCloudView projectSelectionCloudView = new(_mainMenuWindow);
        _mainMenuWindow.CurrentUserControl = projectSelectionCloudView;
    }

    private void MainOpenProjectButton_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void MainSettingsButton_OnClick(object? sender, RoutedEventArgs e)
    {
        SettingsWindowV2 settingsWindow = new();
        _mainMenuWindow.Hide();
        settingsWindow.Show();
        
    }
    #endregion

    #region Other Controls
    private void MainExitButton_OnClick(object? sender, RoutedEventArgs e)
    {
        ApplicationControl.ExitApplication();
    }
    #endregion
}