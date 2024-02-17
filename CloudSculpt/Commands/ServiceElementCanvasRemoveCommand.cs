using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using CloudSculpt.Events;
using CloudSculpt.HelperClasses;
using CloudSculpt.ViewModels;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace CloudSculpt.Commands;

public class ServiceElementCanvasRemoveCommand (ServiceElementViewModel element) : CommandBase
{
    public override async void Execute(object? parameter)
    {
        var box = MessageBoxManager
            .GetMessageBoxStandard("Warning", $"Are you sure you want to remove element : ({element.Text})?" +
                                              $"\nNote:- This action CANNOT be reversed!",
                ButtonEnum.YesNo,
                Icon.Warning);

        var result = await box.ShowAsync();

        if (result == ButtonResult.No) return;
        
        EventAggregator.Instance.Publish(new WaitCursorChangeEvent(true));
        
        var elementIndex = element.ElementIndex;
        var containerId = element.ContainerId;
        
        // Remove Container
        if (!string.IsNullOrWhiteSpace(containerId))
        {
            await DockerManage.RemoveContainer(containerId);
        }
        
        EventAggregator.Instance.Publish(new RemoveServiceElementEvent(elementIndex));
        EventAggregator.Instance.Publish(new WaitCursorChangeEvent(false));
    }
}