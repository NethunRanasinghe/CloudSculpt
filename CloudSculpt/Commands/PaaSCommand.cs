using System;
using System.Collections.Generic;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class PaaSCommand (ConfigureCloudInfraViewModel configureCloudInfraViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        // Set Button Opacity
        configureCloudInfraViewModel.PaaSOpacity = 0.6;
        configureCloudInfraViewModel.IaaSOpacity = 1.0;
        
        Dictionary<string, string> paaSComponents = new Dictionary<string, string>()
        {
            {"Containers", "avares://CloudSculpt/Assets/dockerBlack.png"},
            {"Kubernetes", "avares://CloudSculpt/Assets/kubernetesBlack.png"}
        };
        
        // Clear collection
        configureCloudInfraViewModel.ServiceElements.Clear();
        
        foreach (var component in paaSComponents)
        {
            var serviceElementViewModel = new ServiceElementViewModel
            {
                Text = component.Key,
                Image = new Bitmap(AssetLoader.Open(new Uri(component.Value)))
            };
            
            configureCloudInfraViewModel.ServiceElements.Add(serviceElementViewModel);
        }
    }
}