using Avalonia.Input;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ServiceElementCanvasPointerMoved (ServiceElementViewModel element) : CommandBase
{
    public override void Execute(object? parameter)
    {
        if (parameter is not PointerEventArgs e) return;
        if (!element.IsPressed) return;
        
        var point = e.GetCurrentPoint(null);
        var x = point.Position.X;
        var y = point.Position.Y;

        var offsetX = x - element.ElementClickedLeft;
        var offsetY = y - element.ElementClickedTop;

        element.CanvasLeft += offsetX;
        element.CanvasTop += offsetY;

        // Update the initial click position for the next move event
        element.ElementClickedLeft = x;
        element.ElementClickedTop = y;
    }
}