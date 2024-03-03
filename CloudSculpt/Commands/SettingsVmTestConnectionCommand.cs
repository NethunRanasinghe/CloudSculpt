using CloudSculpt.HelperClasses;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace CloudSculpt.Commands;

public class SettingsVmTestConnectionCommand : CommandBase
{
    public override async void Execute(object? parameter)
    {
        // Check Docker Engine Connection
        var selectedIp = DatabaseManage.SelectedConfig.vmIp;
        if (!string.IsNullOrWhiteSpace(selectedIp))
        {
            var result = await DockerManage.VerifyDockerStatus();
            if (!string.IsNullOrWhiteSpace(result)) return;
            var box = MessageBoxManager
                .GetMessageBoxStandard("Success",
                    $"Docker Connection Successful !\nIP :- {selectedIp}",
                    ButtonEnum.Ok, Icon.Success);

            await box.ShowAsync();
        }
        else
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Error (G001)",
                    "No Selected Config !, Please set the IP for the docker engine.",
                    ButtonEnum.Ok, Icon.Success);

            await box.ShowAsync();
        }
        
    }
}