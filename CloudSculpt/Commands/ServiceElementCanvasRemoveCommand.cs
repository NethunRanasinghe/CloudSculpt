using CloudSculpt.Events;
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
            var dockerHelper = new HelperClasses.Docker();
            await dockerHelper.StopContainer(containerId);
            await dockerHelper.RemoveContainer(containerId);
        }
        
        EventAggregator.Instance.Publish(new RemoveServiceElementEvent(elementIndex));
    }
}