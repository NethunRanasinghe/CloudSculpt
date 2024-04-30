using CloudSculpt.ViewModels;
using CloudSculpt.Views.UserControls;

namespace CloudSculpt.Commands;

public class KubernetesEnvironmentConfigNamespaceCustomCommand (KubernetesEnvironmentConfigViewModel kubernetesEnvironmentConfigViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        if (kubernetesEnvironmentConfigViewModel.CurrentNamespaceButtonText.Equals("Default"))
        {
            kubernetesEnvironmentConfigViewModel.CurrentNamespacePodControl =
                new KubeEnvironmentConfigNamespaceCustom();
            kubernetesEnvironmentConfigViewModel.CurrentNamespaceButtonText = "Custom";
        }
        else
        {
            kubernetesEnvironmentConfigViewModel.CurrentNamespacePodControl =
                new KubeEnvironmentConfigNamespaceDefault();
            kubernetesEnvironmentConfigViewModel.CurrentNamespaceButtonText = "Default";
        }
    }
}