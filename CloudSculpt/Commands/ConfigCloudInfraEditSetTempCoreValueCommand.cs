using Avalonia.Controls;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraEditSetTempCoreValueCommand(ServiceElementViewModel serviceElementViewModel)  : CommandBase
{
    public override void Execute(object? parameter)
    {
        if(parameter is not TextBox textBox) return;
        if(textBox.Text == null) return;
        serviceElementViewModel.TempCoreCount = textBox.Text;
    }
}