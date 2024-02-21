using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class SettingsMainBackCommand(SettingsV2ViewModel settingsV2ViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        settingsV2ViewModel.NavigationService.NavigateTo("MainMenuWindow");
    }
}