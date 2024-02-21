using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia.Controls;
using CloudSculpt.Commands;
using CloudSculpt.Views.UserControls;

namespace CloudSculpt.ViewModels;

public class SettingsV2ViewModel : ViewModelBase
{
    private string _settingsTitle;
    private UserControl _currentSettingsView;
    private ObservableCollection<string> _themeTypes;
    private string _selectedTheme;

    public string SelectedTheme
    {
        get => _selectedTheme;
        set => SetField(ref _selectedTheme, value);
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
    
    public ICommand SettingsMainVmSettings { get; }
    public ICommand SettingsMainDockerSettings { get; }
    public ICommand SettingsMainUiSettings { get; }
    public ICommand SettingsMainDiagnostics { get; }
    public ICommand SettingsMainBack { get; }
    public ICommand SettingsContentBack { get; }
    
    public SettingsV2ViewModel()
    {
        // Initial Values
        SettingsTitle = "Settings";
        CurrentSettingsView = new SettingsMainView();
        ThemeTypes = ["Light", "Dark", "Default"];
        SelectedTheme = "Light";
        
        // Commands
        SettingsMainVmSettings = new SettingsMainVmCommand(this);
        SettingsMainDockerSettings = new SettingsMainDockerCommand(this);
        SettingsMainUiSettings = new SettingsMainUiCommand(this);
        SettingsMainDiagnostics = new SettingsMainDiagnosticCommand(this);
        SettingsMainBack = new SettingsMainBackCommand();
        SettingsContentBack = new SettingsContentBackCommand(this);
    }
}