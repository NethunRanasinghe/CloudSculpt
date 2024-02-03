using Avalonia.Input;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ServiceElementCanvasPointerMoved (ServiceElementViewModel element) : CommandBase
{
    public override void Execute(object? parameter)
    {
        if (parameter is PointerEventArgs e)
        {
            if (!element.IsPressed) return;
            var point = e.GetCurrentPoint(null);
            var x = point.Position.X;
            var y = point.Position.Y;

            element.CanvasLeft = x;
            element.CanvasTop = y;
        }
    }
}