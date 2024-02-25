using CloudSculpt.HelperClasses;
using CloudSculpt.ViewModels;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace CloudSculpt.Commands;

public class SettingsVmRemoveCommand(SettingsV2ViewModel settingsV2ViewModel) : CommandBase
{
    public override async void Execute(object? parameter)
    {
        if(string.IsNullOrWhiteSpace(settingsV2ViewModel.VmIpAddress)) return;            
        // Check if it exists
        var allVmData = DatabaseManage.AllVms;
        var ipAddress = settingsV2ViewModel.VmIpAddress;
        var isExist = false;
        foreach (var vm in allVmData)
        {
            if (!vm.vmIp.Equals(ipAddress)) continue;
            isExist = true;
        }
        
        // Delete if exists
        if (isExist)
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Warning", $"Are you sure you want to delete '{settingsV2ViewModel.VmIpAddress}' from saved configurations ?",
                    ButtonEnum.YesNo,Icon.Warning);

            var result = await box.ShowAsync();
            if (result == ButtonResult.No) return;
            var rows = await DatabaseManage.RemoveVmData(settingsV2ViewModel.VmIpAddress);
            if(rows <= 0) return;
            
            settingsV2ViewModel.VmIpAddress = string.Empty;
            settingsV2ViewModel.VmConfigName = string.Empty;
            
            var box2 = MessageBoxManager
                .GetMessageBoxStandard("Success", "Configuration has been deleted successfully !",ButtonEnum.Ok,Icon.Success);
            await box2.ShowAsync();

            await DatabaseManage.GetAllVmData();
            settingsV2ViewModel.AllVms = DatabaseManage.AllVms;
        }
        else
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Error", $"There are no configurations with the ip '{settingsV2ViewModel.VmIpAddress}'",ButtonEnum.Ok,Icon.Error);
            await box.ShowAsync();
        }
    }
}