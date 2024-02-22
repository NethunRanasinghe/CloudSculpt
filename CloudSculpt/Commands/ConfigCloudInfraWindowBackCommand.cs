using CloudSculpt.ViewModels;
using CloudSculpt.Views.UserControls;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraWindowBackCommand(ConfigureCloudInfraViewModel configureCloudInfraViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        configureCloudInfraViewModel.CurrentWindow.Hide();
        configureCloudInfraViewModel.NavigationService.NavigateAndChangeUserControl("MainMenuWindow", new ProjectSelectionCloudView());
    }
}