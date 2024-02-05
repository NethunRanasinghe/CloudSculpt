using CloudSculpt.Events;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ServiceElementAddCommand (ServiceElementViewModel element) : CommandBase
{
    
    public override void Execute(object? parameter)
    {
        var elementIndex = ServiceElementViewModel.ElementCounter;
        var elementName = element.Text;
        var elementImage = element.Image;
        var configType = element.ConfigType;
        
        var serviceElementViewModel = new ServiceElementViewModel
        {
            Text = elementName,
            Image = elementImage,
            ElementIndex = elementIndex,
            ConfigType = configType
        };
        
        EventAggregator.Instance.Publish(new AddServiceElementEvent(serviceElementViewModel));
    }
}