using System.Collections.Generic;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class IaaSCommand (ICollection<ServiceElementViewModel> serviceElements) : CommandBase
{
    public override void Execute(object? parameter)
    {
        Dictionary<string, string> iaaSComponents = new Dictionary<string, string>()
        {
            {"VMs", "res:Assets/dockerBlack.png"}
        };
        
        // Clear collection
        serviceElements.Clear();
        
        foreach (var component in iaaSComponents)
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