using CloudSculpt.ViewModels;
using CloudSculpt.Views.UserControls;

namespace CloudSculpt.Commands;

public class ProjectSelectionCloudViewNetworkingCommand (MainMenuViewModel mainMenuViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        mainMenuViewModel.CurrentUserControl = new ProjectSelectionNetworkView();
    }
}