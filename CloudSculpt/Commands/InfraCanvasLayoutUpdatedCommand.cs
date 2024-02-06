﻿using Avalonia;
using Avalonia.Controls;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class InfraCanvasLayoutUpdatedCommand : CommandBase
{
    public override void Execute(object? parameter)
    {
        if(parameter is not Canvas canvas) return;

        var width = canvas.Bounds.Width;
        var height = canvas.Bounds.Height;

        ServiceElementViewModel.CanvasWidth = width;
        ServiceElementViewModel.CanvasHeight = height;
        
        var screenCoordinates = canvas.PointToScreen(new Avalonia.Point(0, 0));
        ServiceElementViewModel.CanvasScreenX = screenCoordinates.X;
        ServiceElementViewModel.CanvasScreenY = screenCoordinates.Y;
    }
}