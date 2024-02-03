using Avalonia.Input;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ServiceElementCanvasPointerPressed (ServiceElementViewModel element) : CommandBase
{
    public override void Execute(object? parameter)
    {
        if (parameter is PointerPressedEventArgs e)
        {
            // Set Mouse Clicked - Start Position
            var point = e.GetCurrentPoint(null);
            var x = point.Position.X;
            var y = point.Position.Y;
            
            element.ElementClickedTop = y;
            element.ElementClickedLeft = x;
            
            element.IsPressed = true;
        }
    }
}