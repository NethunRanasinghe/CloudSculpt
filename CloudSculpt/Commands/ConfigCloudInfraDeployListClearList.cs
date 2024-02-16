using CloudSculpt.ViewModels;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraDeployListClearList (ConfigureCloudInfraViewModel configureCloudInfraViewModel) : CommandBase
{
    public override async void Execute(object? parameter)
    {
        var box = MessageBoxManager
            .GetMessageBoxStandard("Warning", "Are you sure you want to clear the deploy list?\nNote:- This action CANNOT be reversed!",
                ButtonEnum.YesNo,
                Icon.Warning);

        var result = await box.ShowAsync();

        if (result != ButtonResult.Yes) return;
        configureCloudInfraViewModel.InfraCanvasCollection.Clear();
    }
}