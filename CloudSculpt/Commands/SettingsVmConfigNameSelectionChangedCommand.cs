using Avalonia.Controls;
using CloudSculpt.Models;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class SettingsVmConfigNameSelectionChangedCommand(SettingsV2ViewModel settingsV2ViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        if(parameter is not ComboBox comboBox) return;
        var allVms = settingsV2ViewModel.AllVms;
        
        if(comboBox.SelectedValue is null) return;
        var selectedConfig = (VmData)comboBox.SelectedValue;
        var selectedConfigName = selectedConfig.vmName;
        
        if(string.IsNullOrWhiteSpace(selectedConfigName)) return;
        var selectedVmConfig = allVms.Find(data => data.vmName.Equals(selectedConfigName));
        
        if(selectedVmConfig is null) return;
        settingsV2ViewModel.VmIpAddress = selectedVmConfig.vmIp;
    }
}