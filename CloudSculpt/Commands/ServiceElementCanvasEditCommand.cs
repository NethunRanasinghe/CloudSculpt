using System;
using CloudSculpt.Events;
using CloudSculpt.ViewModels;
using CloudSculpt.Views.Windows;

namespace CloudSculpt.Commands;

public class ServiceElementCanvasEditCommand (ServiceElementViewModel serviceElementViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        var anotherTabOpen = ConfigCloudInfraEditViewModel.IsOneActive;
        if (anotherTabOpen) return;
        var configCloudInfraEditWindow = new ConfigCloudInfraEditWindow();
        EventAggregator.Instance.Publish(new ConfigInfraEditEvent(configCloudInfraEditWindow,serviceElementViewModel));
    }
}