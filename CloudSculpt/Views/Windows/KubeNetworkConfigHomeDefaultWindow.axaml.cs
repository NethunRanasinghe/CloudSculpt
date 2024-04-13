using Avalonia.Controls;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Views.Windows;

public partial class KubeNetworkConfigHomeDefaultWindow : Window
{
    public KubeNetworkConfigHomeDefaultWindow()
    {
        InitializeComponent();
        DataContext = new KubernetesEnvironmentConfigViewModel(this);
    }
}