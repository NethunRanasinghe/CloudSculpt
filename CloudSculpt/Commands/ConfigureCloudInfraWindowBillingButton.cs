using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ConfigureCloudInfraWindowBillingButton (ConfigureCloudInfraViewModel configureCloudInfraViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        configureCloudInfraViewModel.IsBillingSelected = false;
    }
}