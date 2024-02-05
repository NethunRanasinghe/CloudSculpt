using System;
using System.Collections.Generic;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class IaaSCommand (ConfigureCloudInfraViewModel configureCloudInfraViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        // Set Button Opacity
        configureCloudInfraViewModel.IaaSOpacity = 0.6;
        configureCloudInfraViewModel.PaaSOpacity = 1.0;
        
        Dictionary<string, List<string>> iaaSComponents = new Dictionary<string, List<string>>()
        {
            {"VMs", ["avares://CloudSculpt/Assets/vm.png", "VM Config"]}
        };
        
        // Clear collection
        configureCloudInfraViewModel.ServiceElements.Clear();
        
        foreach (var component in iaaSComponents)
        {
            var serviceElementViewModel = new ServiceElementViewModel
            {
                Text = component.Key,
                Image = new Bitmap(AssetLoader.Open(new Uri(component.Value[0]))),
                ConfigType = component.Value[1]
            };
            
            configureCloudInfraViewModel.ServiceElements.Add(serviceElementViewModel);
        }
    }
}