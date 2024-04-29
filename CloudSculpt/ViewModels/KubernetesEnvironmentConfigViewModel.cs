using System.Windows.Input;
using Avalonia.Controls;
using CloudSculpt.Commands;
using CloudSculpt.Interfaces;
using CloudSculpt.Services;
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
    
    public ICommand KubernetesEnvironmentConfigGoBack { get; }
    public ICommand KubernetesEnvironmentConfigCustom { get; }
    public ICommand KubernetesEnvironmentConfigDefault { get; }
    
    public readonly Window CurrentWindow;
    public readonly INavigationService NavigationService;
    public KubernetesEnvironmentConfigViewModel(Window window)
    {
        // Initial Values
        CurrentWindow = window;
        NavigationService = ServiceLocator.Resolve<INavigationService>();
        CurrentDefaultUserControl = new KubeEnvironmentConfigHomeDefaultMain();
        CurrentCustomUserControl = new KubeEnvironmentConfigHomeCustomMain();
        
        // Commands
        KubernetesEnvironmentConfigGoBack = new KubeEnvironmentConfigToNetworkProjectCommand(this);
        KubernetesEnvironmentConfigCustom = new KubeEnvironmentConfigCustomCommands(this);
        KubernetesEnvironmentConfigDefault = new KubernetesEnvironmentConfigDefaultCommand(this);
    }
}