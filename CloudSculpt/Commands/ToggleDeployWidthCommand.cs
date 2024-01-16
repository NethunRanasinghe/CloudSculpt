using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ToggleDeployWidthCommand(ConfigureCloudInfraViewModel configViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        if (configViewModel.SecondColumnDefWidth == 0)
        {
            configViewModel.IsCollapsed = false;
            configViewModel.IsNone = false;
            configViewModel.IsExpand = true;
            
            configViewModel.SecondColumnDefWidth = 363;
            configViewModel.DeployButtonText = "<- Deploy";


        }
        else
        {
            configViewModel.IsExpand = false;
            configViewModel.IsNone = false;
            configViewModel.IsCollapsed = true;
            
            configViewModel.SecondColumnDefWidth = 0;
            configViewModel.DeployButtonText = "Deploy ->";
        }
    }
}