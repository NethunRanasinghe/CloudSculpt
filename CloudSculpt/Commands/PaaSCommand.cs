using System;
using System.Collections.Generic;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class PaaSCommand (ICollection<ServiceElementViewModel> serviceElements) : CommandBase
{
    public override void Execute(object? parameter)
    {
        Dictionary<string, string> paaSComponents = new Dictionary<string, string>()
        {
            {"Containers", "avares://CloudSculpt/Assets/dockerBlack.png"},
            {"Kubernetes", "avares://CloudSculpt/Assets/kubernetesBlack.png"}
        };
        
        // Clear collection
        serviceElements.Clear();
        
        foreach (var component in paaSComponents)
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