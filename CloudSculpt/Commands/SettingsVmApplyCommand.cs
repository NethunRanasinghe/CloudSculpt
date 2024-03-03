using CloudSculpt.HelperClasses;
using CloudSculpt.Models;
using CloudSculpt.ViewModels;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace CloudSculpt.Commands;

public class SettingsVmApplyCommand(SettingsV2ViewModel settingsV2ViewModel) : CommandBase
{
    public override async void Execute(object? parameter)
    {
        var tempSelectedConfig = DatabaseManage.SelectedConfigTemp;
        VmData selectedConfig;
        
        if(string.IsNullOrWhiteSpace(settingsV2ViewModel.VmIpAddress)) return;
        if (tempSelectedConfig is null)
        {
            selectedConfig = new VmData()
            {
                vmIp = settingsV2ViewModel.VmIpAddress,
                vmName = settingsV2ViewModel.VmIpAddress
            };
        }
        else
        {
            if (!string.Equals(tempSelectedConfig.vmIp, settingsV2ViewModel.VmIpAddress))
            {
                selectedConfig = new VmData()
                {
                    vmIp = settingsV2ViewModel.VmIpAddress,
                    vmName = settingsV2ViewModel.VmIpAddress
                };
            }
            else
            {
                selectedConfig = DatabaseManage.SelectedConfigTemp;
            }
        }

        
        var box = MessageBoxManager
            .GetMessageBoxStandard("Warning", $"Are you sure you want to set '{selectedConfig.vmIp}' as your docker connection host IP ?",
                ButtonEnum.YesNo);

        var result = await box.ShowAsync();
        if (result != ButtonResult.Yes) return;
        DatabaseManage.SelectedConfig = selectedConfig;
        DockerManage.SetRemoteDockerUri(selectedConfig.vmIp);
        settingsV2ViewModel.IsApplied = true;
    }
}