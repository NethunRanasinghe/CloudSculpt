using Avalonia.Controls;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraEditTerminalKeyDownCommand: CommandBase
{
    public override void Execute(object? parameter)
    {
        if (parameter is not TextBox e) return;
        if (e.DataContext is not ServiceElementViewModel serviceElementViewModel)
            return;
        
        serviceElementViewModel.ConfigCloudInfraTerminalOutput += $"Command >> {e.Text}\n";
        e.Text = string.Empty;
    }
}