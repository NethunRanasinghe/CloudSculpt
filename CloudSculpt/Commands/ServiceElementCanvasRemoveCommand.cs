using CloudSculpt.Events;
using CloudSculpt.HelperClasses;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ServiceElementCanvasRemoveCommand (ServiceElementViewModel element) : CommandBase
{
    public override async void Execute(object? parameter)
    {
        var elementIndex = element.ElementIndex;
        var containerId = element.ContainerId;
        
        // Remove Container
        if (!string.IsNullOrWhiteSpace(containerId))
        {
            await DockerManage.StopContainer(containerId);
            await DockerManage.RemoveContainer(containerId);
        }
        
        EventAggregator.Instance.Publish(new RemoveServiceElementEvent(elementIndex));
    }
}