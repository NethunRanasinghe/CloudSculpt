using System.Windows.Input;
using Avalonia.Controls;
using CloudSculpt.Commands;
using CloudSculpt.Events;
using CloudSculpt.Views.UserControls;
using CloudSculpt.Views.Windows;

namespace CloudSculpt.ViewModels;

public class ConfigCloudInfraEditViewModel : ViewModelBase
{
    private ConfigCloudInfraEditWindow _configCloudInfraEditWindow;
    private UserControl _configCloudInfraEditCurrentView;
    private ViewModelBase _configCloudInfraEditCurrentViewModel;
    private bool _configButtonSelected;
    private bool _terminalButtonSelected;
    public static bool IsOneActive { get; private set; }

    public ConfigCloudInfraEditWindow ConfigCloudInfraEditWindow
    {
        get => _configCloudInfraEditWindow;
        private set => SetField(ref _configCloudInfraEditWindow, value);
    }
    
    public UserControl ConfigCloudInfraEditCurrentView
    {
        get => _configCloudInfraEditCurrentView;
        set => SetField(ref _configCloudInfraEditCurrentView, value);
    }

    public ViewModelBase ConfigCloudInfraEditCurrentViewModel
    {
        get => _configCloudInfraEditCurrentViewModel;
        private set => SetField(ref _configCloudInfraEditCurrentViewModel, value);
    }
    
    public bool ConfigButtonSelected
    {
        get => _configButtonSelected;
        set => SetField(ref _configButtonSelected, value);
    }
    
    public bool TerminalButtonSelected
    {
        get => _terminalButtonSelected;
        set => SetField(ref _terminalButtonSelected, value);
    }
    
    public ICommand ConfigCloudInfraEditWindowPointerPressedCommand { get; }
    public ICommand ConfigCloudInfraEditWindowTerminalCommand { get; }
    public ICommand ConfigCloudInfraEditWindowConfigCommand { get; }

    public ConfigCloudInfraEditViewModel()
    {
        // Initialize Active Status
        IsOneActive = false;
        
        // Events
        EventAggregator.Instance.Subscribe<ConfigInfraEditEvent>(OnConfigInfraEdit);
        EventAggregator.Instance.Subscribe<ConfigInfraEditCancelEvent>(OnConfigInfraEditCancel);
        
        // Set Initial View
        ConfigCloudInfraEditCurrentView = new ConfigCloudInfraEditConfigView();
        
        // Initial Button Selection
        ConfigButtonSelected = true;
        TerminalButtonSelected = false;
        
        // Commands
        ConfigCloudInfraEditWindowPointerPressedCommand = new ConfigCloudInfraEditWindowPointerPressedCommand(this);
        ConfigCloudInfraEditWindowTerminalCommand = new ConfigCloudInfraEditWindowTerminalCommand(this);
        ConfigCloudInfraEditWindowConfigCommand = new ConfigCloudInfraEditWindowConfigCommand(this);
    }

    private void OnConfigInfraEdit(ConfigInfraEditEvent obj)
    {
        ConfigCloudInfraEditWindow = obj.ConfigCloudInfraEditWindow;
        ConfigCloudInfraEditCurrentViewModel = obj.ServiceElementViewModel;

        ConfigCloudInfraEditCurrentView.DataContext = ConfigCloudInfraEditCurrentViewModel;
        
        IsOneActive = true;
        ConfigCloudInfraEditWindow.Show();
    }
    
    private void OnConfigInfraEditCancel(ConfigInfraEditCancelEvent obj)
    {
        if(!obj.CancelEvent) return;
        IsOneActive = false;
        ConfigCloudInfraEditWindow.Close();
    }
}