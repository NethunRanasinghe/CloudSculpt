using Avalonia.Controls;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Views.Windows;

public partial class ConfigureCloudInfraWindow : Window
{
    public ConfigureCloudInfraWindow()
    {
        InitializeComponent();
        DataContext = new ConfigureCloudInfraViewModel(this);
    }
}