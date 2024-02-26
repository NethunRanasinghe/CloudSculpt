using CloudSculpt.HelperClasses;
using CloudSculpt.ViewModels;
using CloudSculpt.Views.UserControls;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraEditWindowTerminalStartCommand (ServiceElementViewModel serviceElementViewModel) : CommandBase
{
    public override async void Execute(object? parameter)
    {
        var selectedConfig = DatabaseManage.SelectedConfig;
        if (string.IsNullOrWhiteSpace(selectedConfig.vmIp))
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Error", "You haven't selected or configured a VM !. Please configure it in settings to continue...",
                    ButtonEnum.Ok,Icon.Warning);

            await box.ShowAsync();
            return;
        }
        
        var dockerStatus = await DockerManage.VerifyDockerStatus();
        if (!string.IsNullOrWhiteSpace(dockerStatus)) return;
        
        // Get Element Type
        var elementType = serviceElementViewModel.ElementType;

        if (elementType.Equals("v"))
        {
            // Info
            var distro = serviceElementViewModel.Distro;
            var tag = serviceElementViewModel.Tag;
        
            // Initial Text
            var greetingText = "" +
                               "-----------------\n" +
                               "Os: Linux\n" +
                               $"Distro: {distro}\n" +
                               $"Version: {tag}\n" +
                               "-----------------\n\n";

            if (!serviceElementViewModel.HasGreeted)
            {
                serviceElementViewModel.ConfigCloudInfraTerminalOutput += greetingText;
                serviceElementViewModel.HasGreeted = true;
            }
        
            serviceElementViewModel.ConfigCloudInfraTerminalOutput += "Status: Starting....\n";
        
            // Start the container
            if (string.IsNullOrWhiteSpace(serviceElementViewModel.ContainerId))
            {
                await DockerManage.PullImage(distro,tag);
                var containerId = await DockerManage.CreateContainer($"{distro}:{tag}");
                serviceElementViewModel.ContainerId = containerId;
            }
        
            await DockerManage.StartContainer(serviceElementViewModel.ContainerId);
            serviceElementViewModel.ConfigCloudInfraTerminalOutput += "Status: Started !\n";
        
            // Enable the command TextBox
            serviceElementViewModel.HasStarted = true;
        }

        if (elementType.Equals("d"))
        {
            var filePath = serviceElementViewModel.DockerFilePath;
            serviceElementViewModel.ConfigCloudInfraTerminalOutput = string.Empty;
            if(string.IsNullOrWhiteSpace(filePath)) return;
            
            var imageName = serviceElementViewModel.ImageName;
            serviceElementViewModel.HasStarted = false;
            serviceElementViewModel.ButtonState = false;
            var dockerFileCopyDirs = serviceElementViewModel.DockerFileCopyDirs;
            
            var builtProgress = await DockerManage.BuildDockerFile(filePath,$"{imageName}:latest", dockerFileCopyDirs);
            if(string.IsNullOrWhiteSpace(builtProgress)) return;
            
            // Create and Start Container
            var containerId = await DockerManage.CreateContainer($"{imageName}:latest");
            serviceElementViewModel.ContainerId = containerId;
            await DockerManage.StartContainer(containerId);
            
            // Initial Text
            var greetingText = "" +
                               "--------Build Progress--------\n" +
                               $"{builtProgress}" +
                               "------------------------------\n\n";

            serviceElementViewModel.ConfigCloudInfraTerminalOutput += greetingText;
            serviceElementViewModel.HasGreeted = true;
            serviceElementViewModel.HasStarted = true;
            serviceElementViewModel.ButtonState = true;
        }
    }
}