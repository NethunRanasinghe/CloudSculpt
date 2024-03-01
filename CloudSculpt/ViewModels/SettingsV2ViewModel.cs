using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using CloudSculpt.Commands;
using CloudSculpt.HelperClasses;
using CloudSculpt.Interfaces;
using CloudSculpt.Models;
using CloudSculpt.Services;
using CloudSculpt.Views.UserControls;

namespace CloudSculpt.ViewModels;

public class SettingsV2ViewModel : ViewModelBase
{
    private string _settingsTitle;
    private UserControl _currentSettingsView;
    private ObservableCollection<string> _themeTypes;
    private string _selectedTheme;
    private List<VmData> _allVms;
    private string _vmIpAddress;
    private string _vmConfigName;
    private List<string> _vmNames;

    public string SelectedTheme
    {
        get => _selectedTheme;
        set
        {
            SetCurrentTheme(value);
            SetField(ref _selectedTheme, value);
        }
    }

    public ObservableCollection<string> ThemeTypes
    {
        get => _themeTypes;
        set => SetField(ref _themeTypes, value);
    }
    public UserControl CurrentSettingsView
    {
        get => _currentSettingsView;
        set => SetField(ref _currentSettingsView, value);
    }

    public string SettingsTitle
    {
        get => _settingsTitle;
        set => SetField(ref _settingsTitle, value);
    }
    
    public List<VmData> AllVms
    {
        get => _allVms;
        set
        {
            if (SetField(ref _allVms, value))
            {
                
                OnPropertyChanged(nameof(VmNames));
            }
        }
    }
    
    public string VmIpAddress
    {
        get => _vmIpAddress;
        set => SetField(ref _vmIpAddress, value);
    }
    
    public string VmConfigName
    {
        get => _vmConfigName;
        set => SetField(ref _vmConfigName, value);
    }
    
    public List<string> VmNames
    {
        get
        {
            List<string> names = [];
            names.AddRange(AllVms.Select(vm => vm.vmName));
            return names;
        }
    }
    
    public readonly INavigationService NavigationService;
    public readonly Window CurrentWindow;

    public ICommand SettingsMainVmSettings { get; }
    public ICommand SettingsMainDockerSettings { get; }
    public ICommand SettingsMainUiSettings { get; }
    public ICommand SettingsMainDiagnostics { get; }
    public ICommand SettingsMainBack { get; }
    public ICommand SettingsContentBack { get; }
    public ICommand SettingsVmConfigNameSelectionChanged { get; }
    public ICommand SettingsVmApply { get; }
    public ICommand SettingsVmTestConnection { get; }
    public ICommand SettingsVmRemove { get; }
    public ICommand SettingsVmSave { get; }
    
    public SettingsV2ViewModel(Window currentWindow)
    {
        // Initial Values
        NavigationService = ServiceLocator.Resolve<INavigationService>();
        SettingsTitle = "Settings";
        CurrentSettingsView = new SettingsMainView();
        ThemeTypes = ["Light", "Dark", "Default"];
        CurrentWindow = currentWindow;
        AllVms = DatabaseManage.AllVms;
        VmIpAddress = DatabaseManage.SelectedConfig.vmIp;
        VmConfigName = string.Empty;
        
        var savedTheme = ThemeHelper.GetThemeFromSettings();
        SelectedTheme = string.IsNullOrWhiteSpace(savedTheme) ? "Default" : savedTheme;
        
        // Commands
        SettingsMainVmSettings = new SettingsMainVmCommand(this);
        SettingsMainDockerSettings = new SettingsMainDockerCommand(this);
        SettingsMainUiSettings = new SettingsMainUiCommand(this);
        SettingsMainDiagnostics = new SettingsMainDiagnosticCommand(this);
        SettingsMainBack = new SettingsMainBackCommand(this);
        SettingsContentBack = new SettingsContentBackCommand(this);
        SettingsVmConfigNameSelectionChanged = new SettingsVmConfigNameSelectionChangedCommand(this);
        SettingsVmApply = new SettingsVmApplyCommand(this);
        SettingsVmRemove = new SettingsVmRemoveCommand(this);
        SettingsVmSave = new SettingsVmSaveCommand(this);
        SettingsVmTestConnection = new SettingsVmTestConnectionCommand();
    }

    private static void SetCurrentTheme(string selectedTheme)
    {
        ThemeHelper.ChangeCurrentTheme(selectedTheme);
    }
}