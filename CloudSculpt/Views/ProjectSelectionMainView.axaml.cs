using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CloudSculpt.HelperClasses;

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

    private async void MainOpenProjectButton_OnClick(object? sender, RoutedEventArgs e)
    {
        //throw new System.NotImplementedException();
        HelperClasses.Docker docker = new HelperClasses.Docker();

        await docker.BuildDockerFile(@"C:\MyData\Projects\Docker\Test\dockerfileTest","v1.0");
        Console.WriteLine("Done !");
    }

    private void MainSettingsButton_OnClick(object? sender, RoutedEventArgs e)
    {
        SettingsWindow settingsWindow = new();
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