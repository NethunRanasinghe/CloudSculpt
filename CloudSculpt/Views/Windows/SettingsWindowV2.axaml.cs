using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Views.Windows;

public partial class SettingsWindowV2 : Window
{
    public SettingsWindowV2()
    {
        InitializeComponent();
        DataContext = new SettingsV2ViewModel();
    }
}