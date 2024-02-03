using System;
using Avalonia.Input;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ServiceElementCanvasPointerReleased (ServiceElementViewModel element) : CommandBase
{
    public override void Execute(object? parameter)
    {
        if (parameter is PointerReleasedEventArgs e)
        {
            element.IsPressed = false;
        }
    }
}