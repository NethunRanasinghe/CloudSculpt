using System.Windows.Input;
using Avalonia.Controls;
using CloudSculpt.Commands;
using CloudSculpt.Interfaces;
using CloudSculpt.Services;
using CloudSculpt.Views.UserControls;

namespace CloudSculpt.ViewModels;

public class MainMenuViewModel : ViewModelBase
{
    private UserControl _currentUserControl;

    public UserControl CurrentUserControl
    {
        get => _currentUserControl;
        set => SetField(ref _currentUserControl, value);
    }
    
    public ICommand ProjectSelectionMainViewNewProj { get; }
    public ICommand ProjectSelectionMainViewSettings { get; }
    public ICommand ProjectSelectionMainViewExit { get; }
    public ICommand ProjectSelectionCloudViewConfigCloudInfra { get; }
    public ICommand ProjectSelectionCloudViewNetworking { get; }
    public ICommand ProjectSelectionNetworkingViewCloud { get; }
    public ICommand ProjectSelectionBackToMain { get; }
    public ICommand MainWindowInitialized { get; }
    public ICommand ProjectSelectionKubeNetworkConfigDefault { get; }
    
    public readonly INavigationService NavigationService;
    public readonly Window CurrentWindow;

    public MainMenuViewModel(Window window)
    {
        // Initial Value
        CurrentUserControl = new ProjectSelectionMainView();
        NavigationService = ServiceLocator.Resolve<INavigationService>();
        CurrentWindow = window;
        
        // Commands
        ProjectSelectionMainViewNewProj = new ProjectSelectionMainViewNewProjCommand(this);
        ProjectSelectionMainViewSettings = new ProjectSelectionMainViewSettingsCommand(this);
        ProjectSelectionMainViewExit = new ProjectSelectionMainViewExitCommand();
        ProjectSelectionCloudViewConfigCloudInfra = new ProjectSelectionCloudViewConfigCloudInfraCommand(this);
        ProjectSelectionCloudViewNetworking = new ProjectSelectionCloudViewNetworkingCommand(this);
        ProjectSelectionNetworkingViewCloud = new ProjectSelectionNetworkingViewCloudCommand(this);
        ProjectSelectionBackToMain = new ProjectSelectionBackToMainCommand(this);        
        ProjectSelectionKubeNetworkConfigDefault = new ProjectSelectionKubeNetworkConfigDefaultCommand(this);
        MainWindowInitialized = new MainWindowLoadedCommand();
    }
}