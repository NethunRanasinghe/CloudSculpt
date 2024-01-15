﻿using Avalonia.Controls;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ToggleDeployWidthCommand(ConfigureCloudInfraViewModel configViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        if (configViewModel.SecondColumnDefWidth.Value == 0)
        {
            configViewModel.SecondColumnDefWidth = new GridLength(363);
        }
        else
        {
            configViewModel.SecondColumnDefWidth = new GridLength(0);
        }
    }
}