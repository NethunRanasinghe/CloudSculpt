using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CloudSculpt.HelperClasses;
using CloudSculpt.Models;

namespace CloudSculpt;

public partial class SettingsWindow : Window
{
    
    public SettingsWindow()
    {
        InitializeComponent();

        SetOperatingSystem();
        SetDockerStatus();
    }

    #region Navigation
    private void SettingsBackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        MainMenuWindow mainMenuWindow = new();
        this.Hide();
        mainMenuWindow.Show();
    }
    #endregion

    #region Intialize

    private void SetOperatingSystem()
    {
        SettingsOsTextBlock.Text = DockerStatus.Os;
    }

    private void SetDockerStatus()
    {
        SettingsDockerStatusTextBlock.Text = DockerStatus.Status;
    }
    #endregion
}