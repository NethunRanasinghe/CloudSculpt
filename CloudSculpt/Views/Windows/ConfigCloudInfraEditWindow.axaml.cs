using Avalonia.Controls;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Views.Windows;

public partial class ConfigCloudInfraEditWindow : Window
{
    public ConfigCloudInfraEditWindow()
    {
        InitializeComponent();
        DataContext = new ConfigCloudInfraEditViewModel();
    }
}