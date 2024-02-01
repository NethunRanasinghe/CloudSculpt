using CloudSculpt.Events;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ServiceElementCommand (ServiceElementViewModel element) : CommandBase
{
    
    public override void Execute(object? parameter)
    {
        var elementName = element.Text;
        var elementImage = element.Image;
        
        var serviceElementViewModel = new ServiceElementViewModel
        {
            Text = elementName,
            Image = elementImage
        };
        
        EventAggregator.Instance.Publish(new AddServiceElementEvent(serviceElementViewModel));
    }
}