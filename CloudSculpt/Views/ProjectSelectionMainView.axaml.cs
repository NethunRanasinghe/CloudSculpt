using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace CloudSculpt.Views;

public partial class ProjectSelectionMainView : UserControl
{
    private readonly MainMenuWindow _mainMenuWindow;
    
    public ProjectSelectionMainView(MainMenuWindow mainMenuWindow)
    {
        InitializeComponent();
        _mainMenuWindow = mainMenuWindow;
    }

    #region Navigation
    private void MainNewProjectButton_OnClick(object? sender, RoutedEventArgs e)
    {
        ProjectSelectionCloudView projectSelectionCloudView = new(_mainMenuWindow);
        _mainMenuWindow.CurrentUserControl = projectSelectionCloudView;
    }

    private void MainOpenProjectButton_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void MainSettingsButton_OnClick(object? sender, RoutedEventArgs e)
    {
        SettingsWindow settingsWindow = new();
        _mainMenuWindow.Hide();
        settingsWindow.Show();
        
    }
    #endregion

    #region Actions
    private void MainExitButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopApp)
        {
            desktopApp.Shutdown();
        }
    }
    #endregion
}