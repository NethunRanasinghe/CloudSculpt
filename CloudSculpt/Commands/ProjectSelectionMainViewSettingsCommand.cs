using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ProjectSelectionMainViewSettingsCommand (MainMenuViewModel mainMenuViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        mainMenuViewModel.NavigationService.NavigateTo("SettingsWindow");
    }
}