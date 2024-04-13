using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Views.Windows;

public partial class KubeNetworkConfigHomeCustomWindow : Window
{
    public KubeNetworkConfigHomeCustomWindow()
    {
        InitializeComponent();
        DataContext = new KubernetesEnvironmentConfigViewModel(this);
    }
}