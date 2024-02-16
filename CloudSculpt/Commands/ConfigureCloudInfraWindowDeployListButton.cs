using CloudSculpt.ViewModels;
using CloudSculpt.Views.UserControls;

namespace CloudSculpt.Commands;

public class ConfigureCloudInfraWindowDeployListButton (ConfigureCloudInfraViewModel configureCloudInfraViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        configureCloudInfraViewModel.IsBillingSelected = false;
        configureCloudInfraViewModel.BillingCalView = new ConfigCloudInfraDeployList();
    }
}