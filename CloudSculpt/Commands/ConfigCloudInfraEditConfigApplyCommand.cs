using System;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraEditConfigApplyCommand (ServiceElementViewModel serviceElementViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        // Apply Changes
        
        //Name
        serviceElementViewModel.Text = serviceElementViewModel.TempName;
        
        // Distro and Tag
        var tempDistro = serviceElementViewModel.TempDistro;
        if(string.IsNullOrWhiteSpace(tempDistro)) return;
        tempDistro = tempDistro.Trim();
        var tempDistroSplit = tempDistro.Split(' ');
        serviceElementViewModel.Distro = tempDistroSplit[0];
        serviceElementViewModel.Tag = tempDistroSplit[1];
        
        Console.WriteLine(tempDistroSplit[0]);
        Console.WriteLine(tempDistroSplit[1]);
    }
}