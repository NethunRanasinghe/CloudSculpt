using Avalonia.Controls;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraEditWindowOsSelChangedCommand(ServiceElementViewModel serviceElementViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        if(parameter is not ComboBox c) return;
        if(c.SelectedItem == null) return;

        serviceElementViewModel.TempOsType = c.SelectedItem.ToString()!;
        
        if (!c.SelectedItem.Equals("linux"))
        {
            serviceElementViewModel.TempIsLinux = false;
            serviceElementViewModel.TempDistro = string.Empty;
            serviceElementViewModel.TempTag = string.Empty;
            return;
        }
        
        serviceElementViewModel.TempIsLinux = true;
        if (!string.IsNullOrWhiteSpace(serviceElementViewModel.TempDistro)) return;
        serviceElementViewModel.TempDistro = ServiceElementViewModel.DefaultDistro;
        serviceElementViewModel.TempTag = ServiceElementViewModel.DefaultTag;
    }
}