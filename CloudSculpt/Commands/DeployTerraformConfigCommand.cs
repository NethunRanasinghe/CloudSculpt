using CloudSculpt.HelperClasses;
using CloudSculpt.ViewModels;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace CloudSculpt.Commands;

public class DeployTerraformConfigCommand (ConfigureCloudInfraViewModel configureCloudInfraViewModel) : CommandBase
{
    public override async void Execute(object? parameter)
    {
        var box = MessageBoxManager
            .GetMessageBoxStandard("Warning", "All Above Elements Will be Deployed to AWS !",
                ButtonEnum.YesNo, Icon.Error);

        await box.ShowAsync();

        await TerraformHelper.DeployToTerraform(configureCloudInfraViewModel.ServiceElements);
    }
}