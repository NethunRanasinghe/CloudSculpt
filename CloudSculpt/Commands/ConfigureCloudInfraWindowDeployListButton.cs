using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ConfigureCloudInfraWindowDeployListButton (ConfigureCloudInfraViewModel configureCloudInfraViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        configureCloudInfraViewModel.IsBillingSelected = false;
    }
}