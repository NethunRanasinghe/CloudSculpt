using Avalonia.Controls;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraEditWindowDistroSelChangedCommand(ServiceElementViewModel serviceElementViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        if(parameter is not ComboBox c) return;
        if(c.SelectedItem == null) return;

        serviceElementViewModel.TempDistro = c.SelectedItem.ToString() ?? string.Empty;
    }
}