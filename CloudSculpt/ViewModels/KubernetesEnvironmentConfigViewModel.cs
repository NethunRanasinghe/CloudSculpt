using Avalonia.Controls;
using CloudSculpt.Views.UserControls;

namespace CloudSculpt.ViewModels;

public class KubernetesEnvironmentConfigViewModel : ViewModelBase
{
    private UserControl _currentDefaultUserControl;
    private UserControl _currentCustomUserControl;

    public UserControl CurrentDefaultUserControl
    {
        get => _currentDefaultUserControl;
        set => SetField(ref _currentDefaultUserControl, value);
    }
    
    public UserControl CurrentCustomUserControl
    {
        get => _currentCustomUserControl;
        set => SetField(ref _currentCustomUserControl, value);
    }
    
    public readonly Window CurrentWindow;
    public KubernetesEnvironmentConfigViewModel(Window window)
    {
        // Initial Values
        CurrentWindow = window;
        CurrentDefaultUserControl = new KubeEnvironmentConfigHomeDefaultMain();
        CurrentCustomUserControl = new KubeEnvironmentConfigHomeCustomMain();
    }
}