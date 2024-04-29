using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class KubeEnvironmentConfigCustomCommands (KubernetesEnvironmentConfigViewModel kubernetesEnvironmentConfigViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        kubernetesEnvironmentConfigViewModel.CurrentWindow.Hide();
        kubernetesEnvironmentConfigViewModel.NavigationService.NavigateTo("KubeNetworkConfigHomeCustomWindow");
    }
}