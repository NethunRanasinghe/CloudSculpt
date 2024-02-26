using CloudSculpt.HelperClasses;
using CloudSculpt.Models;
using CloudSculpt.ViewModels;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace CloudSculpt.Commands;

public class SettingsVmSaveCommand(SettingsV2ViewModel settingsV2ViewModel) : CommandBase
{
    public override async void Execute(object? parameter)
    {
        if(string.IsNullOrWhiteSpace(settingsV2ViewModel.VmIpAddress)) return;            
        // Check if it already exists
        var allVmData = DatabaseManage.AllVms;
        var ipAddress = settingsV2ViewModel.VmIpAddress;
        var name = settingsV2ViewModel.VmConfigName;
        var isExist = false;
        var existingConfig = new VmData();
        
        foreach (var vm in allVmData)
        {
            if (!vm.vmIp.Equals(ipAddress) && !vm.vmName.Equals(name)) continue;
            existingConfig = vm;
            isExist = true;
        }
        
        // If not save it in the database
        if (!isExist)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = ipAddress;
            }
            
            var box = MessageBoxManager
                .GetMessageBoxStandard("Warning", $"Are you sure you want to save the configuration with name:  '{name}' and ip : '{ipAddress}' ?",
                    ButtonEnum.YesNo,Icon.Warning);

            var result = await box.ShowAsync();
            if (result != ButtonResult.Yes) return;
            
            var rows =await DatabaseManage.InsertVmData(name, ipAddress);
            if(rows <= 0) return;
            
            var box2 = MessageBoxManager
                .GetMessageBoxStandard("Success", "Configuration has been added successfully !",ButtonEnum.Ok,Icon.Success);
            await box2.ShowAsync();
            
            await DatabaseManage.GetAllVmData();
            settingsV2ViewModel.AllVms = DatabaseManage.AllVms;
        }
        else
        {
            var previousName = existingConfig.vmName;
            var previousIp = existingConfig.vmIp;
            var vmId = existingConfig.id;

            if (!previousName.Equals(name) || !previousIp.Equals(ipAddress))
            {
                var box2 = MessageBoxManager
                    .GetMessageBoxStandard("Warning", $"A similar configuration Exists !, Do you want to update it?\n" +
                                                    $"Old Data :- Name : {previousName} || IP : {previousIp}\n" +
                                                    $"New Data :- Name : {name} || IP : {ipAddress}",ButtonEnum.YesNo,Icon.Warning);
                    
                var result = await box2.ShowAsync();
                if(result != ButtonResult.Yes) return;
                var rows = await DatabaseManage.UpdateVmData(name,ipAddress,vmId);
                if(rows <= 0) return;
                
                var box3 = MessageBoxManager
                    .GetMessageBoxStandard("Success", "Configuration has been updated successfully !",ButtonEnum.Ok,Icon.Success);
                await box3.ShowAsync();
            
                await DatabaseManage.GetAllVmData();
                settingsV2ViewModel.AllVms = DatabaseManage.AllVms;
                return;
            }
            
            var box = MessageBoxManager
                .GetMessageBoxStandard("Error", "A similar configuration Exists !",ButtonEnum.Ok,Icon.Error);
            await box.ShowAsync();
        }
        
    }
}