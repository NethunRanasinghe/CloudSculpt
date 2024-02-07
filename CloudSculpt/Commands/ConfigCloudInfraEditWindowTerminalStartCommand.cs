using System;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraEditWindowTerminalStartCommand (ServiceElementViewModel serviceElementViewModel) : CommandBase
{
    public override async void Execute(object? parameter)
    {
        // Initial Text
        var greetingText = "" +
                           "-----------------\n" +
                           "Os: Linux\n" +
                           "Distro: Ubuntu\n" +
                           "-----------------\n\n";
        
        serviceElementViewModel.ConfigCloudInfraTerminalOutput += greetingText;
        
        serviceElementViewModel.ConfigCloudInfraTerminalOutput += "Status: Starting....\n";
        
        // Start the container
        var dockerHelper = new HelperClasses.Docker();
        await dockerHelper.PullImage("ubuntu","latest");
        var containerId = await dockerHelper.CreateContainer("ubuntu:latest");
        Console.WriteLine(containerId);
        serviceElementViewModel.ConfigCloudInfraTerminalOutput += "Status: Started!\n";

        
        // Enable the command TextBox
        serviceElementViewModel.HasStarted = true;
    }
}