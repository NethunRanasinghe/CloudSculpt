using CloudSculpt.Events;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ServiceElementCanvasRemoveCommand (ServiceElementViewModel element) : CommandBase
{
    public override void Execute(object? parameter)
    {
        var elementIndex = element.ElementIndex;
        EventAggregator.Instance.Publish(new RemoveServiceElementEvent(elementIndex));
    }
}