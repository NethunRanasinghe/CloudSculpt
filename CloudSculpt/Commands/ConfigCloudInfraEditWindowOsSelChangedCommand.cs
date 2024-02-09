using Avalonia.Controls;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraEditWindowOsSelChangedCommand(ServiceElementViewModel serviceElementViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        if(parameter is not ComboBox c) return;
        if(c.SelectedItem == null) return;
        
        if (!c.SelectedItem.Equals("linux"))
        {
            serviceElementViewModel.IsLinux = false;
            serviceElementViewModel.Distro = string.Empty;
            serviceElementViewModel.Tag = string.Empty;
            return;
        }
        
        serviceElementViewModel.IsLinux = true;
        serviceElementViewModel.Distro = ServiceElementViewModel.DefaultDistro;
        serviceElementViewModel.Tag = ServiceElementViewModel.DefaultTag;
    }
}