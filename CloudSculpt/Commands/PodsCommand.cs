using System;
using System.Collections.Generic;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class PodsCommand (KubernetesEnvironmentConfigViewModel kubernetesEnvironmentConfigViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        kubernetesEnvironmentConfigViewModel.PodOpacity = 0.6;
        kubernetesEnvironmentConfigViewModel.ConnectionOpacity = 1.0;
        
        Dictionary<string, List<string>> podComponent = new Dictionary<string, List<string>>()
        {
            {"VMs", ["avares://CloudSculpt/Assets/pod.png", "Pod", "p"]}
        };
        
        kubernetesEnvironmentConfigViewModel.ServiceElements.Clear();
        
        foreach (var component in podComponent)
        {
            var serviceElementViewModel = new ServiceElementViewModel
            {
                Text = component.Key,
                TempName = component.Key,
                Image = new Bitmap(AssetLoader.Open(new Uri(component.Value[0]))),
                ConfigType = component.Value[1],
                ElementType = component.Value[2]
            };
            
            kubernetesEnvironmentConfigViewModel.ServiceElements.Add(serviceElementViewModel);
        }
    }
}