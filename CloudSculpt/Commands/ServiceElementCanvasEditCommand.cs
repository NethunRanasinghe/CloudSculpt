using System;
using Avalonia;
using Avalonia.Controls;
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
        const int startupPointOffsetX = 50;
        const int startupPointOffsetY = 50;
        var startupPointLeft = Convert.ToInt32(serviceElementViewModel.CanvasLeft + ServiceElementViewModel.CanvasScreenX) + startupPointOffsetX;
        var startupPointTop = Convert.ToInt32(serviceElementViewModel.CanvasTop + ServiceElementViewModel.CanvasScreenY) + startupPointOffsetY;
        
        configCloudInfraEditWindow.WindowStartupLocation = WindowStartupLocation.Manual;
        configCloudInfraEditWindow.Position = new PixelPoint(startupPointLeft, startupPointTop);

        serviceElementViewModel.IsEditOpen = true;
        EventAggregator.Instance.Publish(new ConfigInfraEditEvent(configCloudInfraEditWindow,serviceElementViewModel));
    }
}