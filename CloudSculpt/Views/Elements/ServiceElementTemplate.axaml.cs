using Avalonia.Controls;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Views.Elements;

public partial class ServiceElementTemplate : UserControl
{
    public ServiceElementTemplate()
    {
        InitializeComponent();
        DataContext = new ServiceElementViewModel();
    }
}