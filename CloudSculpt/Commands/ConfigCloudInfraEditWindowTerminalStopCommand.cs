using System;
using CloudSculpt.HelperClasses;
using CloudSculpt.ViewModels;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraEditWindowTerminalStopCommand (ServiceElementViewModel serviceElementViewModel) : CommandBase
{
    public override async void Execute(object? parameter)
    {
        var dockerStatus = await DockerManage.VerifyDockerStatus();
        if (!string.IsNullOrWhiteSpace(dockerStatus)) return;
        
        var containerId = serviceElementViewModel.ContainerId;
        if(string.IsNullOrWhiteSpace(containerId)) return;
        
        serviceElementViewModel.HasStarted = false;
        serviceElementViewModel.ConfigCloudInfraTerminalOutput += "Status: Stopping....\n";
        try
        {
            await DockerManage.StopContainer(containerId);
            serviceElementViewModel.ConfigCloudInfraTerminalOutput += "Status: Stopped !\n";

        }
        catch (Exception e)
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Error",
                    $"{e.Message}",
                    ButtonEnum.Ok, Icon.Error);

            await box.ShowAsync();
            
            serviceElementViewModel.HasStarted = true;
            serviceElementViewModel.ConfigCloudInfraTerminalOutput += "Status: Failed !\n";
        }
    }
}