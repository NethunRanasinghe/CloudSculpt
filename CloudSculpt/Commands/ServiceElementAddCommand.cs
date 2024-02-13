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
        var elementType = element.ElementType;
        
        var serviceElementViewModel = new ServiceElementViewModel
        {
            Text = elementName,
            TempName = elementName,
            Image = elementImage,
            ElementIndex = elementIndex,
            ConfigType = configType,
            ElementType = elementType,
            IsLinux = false
        };
        
        EventAggregator.Instance.Publish(new AddServiceElementEvent(serviceElementViewModel));
    }
}