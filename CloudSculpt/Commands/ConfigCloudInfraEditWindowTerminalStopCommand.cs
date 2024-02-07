using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraEditWindowTerminalStopCommand (ServiceElementViewModel serviceElementViewModel) : CommandBase
{
    public override async void Execute(object? parameter)
    {
        var containerId = serviceElementViewModel.ContainerId;
        if(string.IsNullOrWhiteSpace(containerId)) return;
        
        serviceElementViewModel.ConfigCloudInfraTerminalOutput += "Status: Stopping....\n";
        var dockerHelper = new HelperClasses.Docker();
        await dockerHelper.StopContainer(containerId);
        serviceElementViewModel.ConfigCloudInfraTerminalOutput += "Status: Stopped !\n";

        serviceElementViewModel.HasStarted = false;
    }
}