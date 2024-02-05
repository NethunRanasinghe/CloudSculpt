using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
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