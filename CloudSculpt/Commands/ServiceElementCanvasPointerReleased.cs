using Avalonia.Input;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ServiceElementCanvasPointerReleased (ServiceElementViewModel element) : CommandBase
{
    public override void Execute(object? parameter)
    {
        if (parameter is not PointerReleasedEventArgs e) return;
        
        element.IsPressed = false;
        
        if(element.IsPressed) return;
            
        var xMaxLimit = ServiceElementViewModel.CanvasWidth;
        var yMaxLimit = ServiceElementViewModel.CanvasHeight;
        const int xMinLimit = 0;
        const int yMinLimit = 0;
            
        var point = e.GetCurrentPoint(null);
        var x = point.Position.X;
        var y = point.Position.Y;

        if (!(x < xMinLimit) && !(y < yMinLimit) && !(x > xMaxLimit) && !(y > yMaxLimit)) return;
            
        // Send element to canvas beginning (0,0)
        element.CanvasLeft = 0;
        element.CanvasTop = 0;
                
        // Update the initial click position for the next move event
        element.ElementClickedLeft = 0;
        element.ElementClickedTop = 0;
    }
}