using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CloudSculpt.HelperClasses;
using CloudSculpt.Models;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

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
        SettingsOsTextBlock.Text = DockerWslStatus.Os;
    }

    private void SetDockerStatus()
    {
        SettingsDockerStatusTextBlock.Text = DockerWslStatus.Status;
    }
    #endregion

    #region Actions

    private async void SettingsInstallButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(DockerWslStatus.Wsl))
        {
            if (DockerWslStatus.Os.Equals("Windows"))
            {
                DockerConfig.DockerInstallWindows();
            }
            else if (DockerWslStatus.Os.Equals("Linux"))
            {
                DockerConfig.DockerInstallLinux();
            }
            else if (DockerWslStatus.Os.Equals("MacOS"))
            {
                DockerConfig.DockerInstallMacOs();
            }
            else
            {
                var box = MessageBoxManager
                    .GetMessageBoxStandard("Error (D002)", "Invalid Os, Only Windows, MacOs and Linux are Supported !",
                        ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);

                var result = await box.ShowAsync();
            }
        }
    }

    #endregion
}