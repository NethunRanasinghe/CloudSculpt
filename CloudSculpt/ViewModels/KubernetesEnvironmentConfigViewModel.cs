﻿using System.Windows.Input;
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
    private UserControl _currentNamespacePodControl;
    private string _currentNamespaceButtonText;

    public string CurrentNamespaceButtonText
    {
        get => _currentNamespaceButtonText;
        set => SetField(ref _currentNamespaceButtonText, value);
    }
    
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

    public UserControl CurrentNamespacePodControl
    {
        get => _currentNamespacePodControl;
        set => SetField(ref _currentNamespacePodControl, value);
    }
    
    public ICommand KubernetesEnvironmentConfigGoBack { get; }
    public ICommand KubernetesEnvironmentConfigCustom { get; }
    public ICommand KubernetesEnvironmentConfigDefault { get; }
    public ICommand KubernetesEnvironmentConfigNamespaceChange { get; }
    
    public readonly Window CurrentWindow;
    public readonly INavigationService NavigationService;
    
    public KubernetesEnvironmentConfigViewModel(Window window)
    {
        // Initial Values
        CurrentWindow = window;
        NavigationService = ServiceLocator.Resolve<INavigationService>();
        CurrentDefaultUserControl = new KubeEnvironmentConfigHomeDefaultMain();
        CurrentCustomUserControl = new KubeEnvironmentConfigHomeCustomMain();
        CurrentNamespacePodControl = new KubeEnvironmentConfigNamespaceDefault();
        CurrentNamespaceButtonText = "Default";
        
        // Commands
        KubernetesEnvironmentConfigGoBack = new KubeEnvironmentConfigToNetworkProjectCommand(this);
        KubernetesEnvironmentConfigCustom = new KubeEnvironmentConfigCustomCommands(this);
        KubernetesEnvironmentConfigDefault = new KubernetesEnvironmentConfigDefaultCommand(this);
        KubernetesEnvironmentConfigNamespaceChange = new KubernetesEnvironmentConfigNamespaceCustomCommand(this);
    }
}