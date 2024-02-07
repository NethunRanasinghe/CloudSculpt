using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraEditWindowTerminalStartCommand (ServiceElementViewModel serviceElementViewModel) : CommandBase
{
    public override async void Execute(object? parameter)
    {
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
        var dockerHelper = new HelperClasses.Docker();
        if (string.IsNullOrWhiteSpace(serviceElementViewModel.ContainerId))
        {
            await dockerHelper.PullImage(distro,tag);
            var containerId = await dockerHelper.CreateContainer($"{distro}:{tag}");
            serviceElementViewModel.ContainerId = containerId;
        }
        
        await dockerHelper.StartContainer(serviceElementViewModel.ContainerId);
        serviceElementViewModel.ConfigCloudInfraTerminalOutput += "Status: Started !\n";
        
        // Enable the command TextBox
        serviceElementViewModel.HasStarted = true;
    }
}