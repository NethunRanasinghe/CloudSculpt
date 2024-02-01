using System;
using System.Collections.Generic;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class IaaSCommand (ICollection<ServiceElementViewModel> serviceElements) : CommandBase
{
    public override void Execute(object? parameter)
    {
        Dictionary<string, string> iaaSComponents = new Dictionary<string, string>()
        {
            {"VMs", "avares://CloudSculpt/Assets/vm.png"}
        };
        
        // Clear collection
        serviceElements.Clear();
        
        foreach (var component in iaaSComponents)
        {
            var serviceElementViewModel = new ServiceElementViewModel
            {
                Text = component.Key,
                Image = new Bitmap(AssetLoader.Open(new Uri(component.Value)))
            };
            
            serviceElements.Add(serviceElementViewModel);
        }
    }
}