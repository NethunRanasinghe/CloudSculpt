using System;
using CloudSculpt.ViewModels;
using CloudSculpt.Views.UserControls;

namespace CloudSculpt.Commands;

public class KubernetesSimulatorGoBackCommand(KubernetesEnvironmentConfigViewModel kubernetesEnvironmentConfigViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        kubernetesEnvironmentConfigViewModel.CurrentWindow.Hide();
        kubernetesEnvironmentConfigViewModel.NavigationService.NavigateAndChangeUserControl("MainMenuWindow", new ProjectSelectionNetworkView());
    }
}