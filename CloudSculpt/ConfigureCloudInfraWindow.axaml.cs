using Avalonia.Controls;
using CloudSculpt.ViewModels;

namespace CloudSculpt;

public partial class ConfigureCloudInfraWindow : Window
{
    public ConfigureCloudInfraWindow()
    {
        InitializeComponent();
        DataContext = new ConfigureCloudInfraViewModel();
    }
}