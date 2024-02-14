using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ConfigureCloudInfraWindowCalculatorButton (ConfigureCloudInfraViewModel configureCloudInfraViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        configureCloudInfraViewModel.IsBillingSelected = true;
    }
}