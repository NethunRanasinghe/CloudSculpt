using System.Collections.Generic;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class PaaSCommand (ICollection<ServiceElementViewModel> serviceElements) : CommandBase
{
    public override void Execute(object? parameter)
    {
        Dictionary<string, string> paaSComponents = new Dictionary<string, string>()
        {
            {"Containers", "res:Assets/dockerBlack.png"},
            {"Kubernetes", "res:Assets/dockerBlack.png"}
        };
        
        // Clear collection
        serviceElements.Clear();
        
        foreach (var component in paaSComponents)
        {
            var serviceElementViewModel = new ServiceElementViewModel
            {
                Text = component.Key,
                //ImagePath = component.Value
            };
            
            serviceElements.Add(serviceElementViewModel);
        }
    }
}