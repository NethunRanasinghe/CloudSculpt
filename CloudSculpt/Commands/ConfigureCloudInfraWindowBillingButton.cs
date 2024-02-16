using CloudSculpt.ViewModels;
using CloudSculpt.Views.UserControls;

namespace CloudSculpt.Commands;

public class ConfigureCloudInfraWindowBillingButton (ConfigureCloudInfraViewModel configureCloudInfraViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        configureCloudInfraViewModel.IsBillingSelected = true;
        configureCloudInfraViewModel.BillingCalView = new ConfigCloudInfraBilling();
    }
}