using System;
using Avalonia.Input;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraEditWindowPointerPressedCommand (ConfigCloudInfraEditViewModel configCloudInfraEditViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        if (parameter is PointerPressedEventArgs e)
        {
            configCloudInfraEditViewModel.ConfigCloudInfraEditWindow.BeginMoveDrag(e);
        }
    }
}