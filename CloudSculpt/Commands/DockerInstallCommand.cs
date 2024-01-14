using CloudSculpt.HelperClasses;
using CloudSculpt.Models;
using CloudSculpt.ViewModels;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace CloudSculpt.Commands;

public class DockerInstallCommand (SettingsViewModel settingsViewModel) : CommandBase
{
    public override async void Execute(object? parameter)
    {
        DockerConfig dockerConfig = new DockerConfig(settingsViewModel);

        if (DockerWslStatus.Status.Equals("Absent"))
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Error (D004)", "Docker Is Already Installed !",
                    ButtonEnum.Ok, Icon.Error);

            await box.ShowAsync();

            return;
        }
        
        if (DockerWslStatus.Os.Equals("Windows"))
        {
            dockerConfig.DockerInstallWindows();
        }
        else if (DockerWslStatus.Os.Equals("Linux"))
        {
            dockerConfig.DockerInstallLinux();
        }
        else if (DockerWslStatus.Os.Equals("MacOS"))
        {
            dockerConfig.DockerInstallMacOs();
        }
        else
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Error (D002)", "Invalid Os, Only Windows, MacOs and Linux are Supported !",
                    ButtonEnum.Ok, Icon.Error);

            await box.ShowAsync();
        }

    }
}