using CloudSculpt.HelperClasses;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraEditWindowTerminalStartCommand (ServiceElementViewModel serviceElementViewModel) : CommandBase
{
    public override async void Execute(object? parameter)
    {
        var dockerStatus = await DockerManage.VerifyDockerStatus();
        if (!string.IsNullOrWhiteSpace(dockerStatus)) return;
        
        // Info
        var distro = serviceElementViewModel.Distro;
        var tag = serviceElementViewModel.Tag;
        
        // Initial Text
        var greetingText = "" +
                           "-----------------\n" +
                           "Os: Linux\n" +
                           $"Distro: {distro}\n" +
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
            await HelperClasses.DockerManage.PullImage(distro,tag);
            var containerId = await DockerManage.CreateContainer($"{distro}:{tag}");
            serviceElementViewModel.ContainerId = containerId;
        }
        
        await DockerManage.StartContainer(serviceElementViewModel.ContainerId);
        serviceElementViewModel.ConfigCloudInfraTerminalOutput += "Status: Started !\n";
        
        // Enable the command TextBox
        serviceElementViewModel.HasStarted = true;
    }
}