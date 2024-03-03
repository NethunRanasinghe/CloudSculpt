using CloudSculpt.ViewModels;
using CloudSculpt.Views.UserControls;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraWindowBackCommand(ConfigureCloudInfraViewModel configureCloudInfraViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        // Check if there's elements in the _infraCanvasCollection
        var canvasCollection = configureCloudInfraViewModel.InfraCanvasCollection;
        if (canvasCollection.Count > 0)
        {
            ConfigureCloudInfraViewModel.InfraCanvasCollectionStatic = canvasCollection;
        }
        
        configureCloudInfraViewModel.CurrentWindow.Hide();
        configureCloudInfraViewModel.NavigationService.NavigateAndChangeUserControl("MainMenuWindow", new ProjectSelectionCloudView());
    }
}