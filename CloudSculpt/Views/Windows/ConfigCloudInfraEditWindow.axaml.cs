using Avalonia.Controls;
using CloudSculpt.Interfaces;
using CloudSculpt.Services;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Views.Windows;

public partial class ConfigCloudInfraEditWindow : Window
{
    public ConfigCloudInfraEditWindow()
    {
        InitializeComponent();
        DataContext = new ConfigCloudInfraEditViewModel();
        ServiceLocator.Register<IFileDialogService>(new FileDialogService(this));
    }
}