using Avalonia.Controls;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Views.Windows;

public partial class KubeNetworkNamespaceCustomizeWindow : Window
{
    public KubeNetworkNamespaceCustomizeWindow()
    {
        InitializeComponent();
        DataContext = new KubernetesEnvironmentConfigViewModel(this);
    }
}