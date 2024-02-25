using Avalonia.Controls;
using CloudSculpt.HelperClasses;
using CloudSculpt.Models;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class SettingsVmConfigNameSelectionChangedCommand(SettingsV2ViewModel settingsV2ViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        if(parameter is not AutoCompleteBox autoCompleteBox) return;
        var allVms = settingsV2ViewModel.AllVms;
        var selectedItem = autoCompleteBox.SelectedItem;
        if(selectedItem is null) return;
        var selectedConfig = new VmData();
        foreach (var vm in allVms)
        {
            if (vm.vmName.Equals(selectedItem))
            {
                selectedConfig = vm;
            }
        }
        var selectedConfigName = selectedConfig.vmName;
        
        if(string.IsNullOrWhiteSpace(selectedConfigName)) return;
        var selectedVmConfig = allVms.Find(data => data.vmName.Equals(selectedConfigName));
        DatabaseManage.SelectedConfigTemp = selectedConfig;
        
        if(selectedVmConfig is null) return;
        settingsV2ViewModel.VmIpAddress = selectedVmConfig.vmIp;
    }
}