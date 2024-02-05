using System.Windows.Input;
using CloudSculpt.Commands;
using CloudSculpt.Events;
using CloudSculpt.Views.UserControls;
using CloudSculpt.Views.Windows;

namespace CloudSculpt.ViewModels;

public class ConfigCloudInfraEditViewModel : ViewModelBase
{
    private ConfigCloudInfraEditWindow _configCloudInfraEditWindow;
    private object _configCloudInfraEditCurrentView;

    public static bool IsOneActive { get; private set; }

    public ConfigCloudInfraEditWindow ConfigCloudInfraEditWindow
    {
        get => _configCloudInfraEditWindow;
        private set => SetField(ref _configCloudInfraEditWindow, value);
    }
    
    public object ConfigCloudInfraEditCurrentView
    {
        get => _configCloudInfraEditCurrentView;
        set => SetField(ref _configCloudInfraEditCurrentView, value);
    }
    
    public ICommand ConfigCloudInfraEditWindowPointerPressedCommand { get; }

    public ConfigCloudInfraEditViewModel()
    {
        IsOneActive = false;
        
        // Commands
        ConfigCloudInfraEditWindowPointerPressedCommand = new ConfigCloudInfraEditWindowPointerPressedCommand(this);
        
        // Events
        EventAggregator.Instance.Subscribe<ConfigInfraEditEvent>(OnConfigInfraEdit);
        EventAggregator.Instance.Subscribe<ConfigInfraEditCancelEvent>(OnConfigInfraEditCancel);
        
        // Set Initial View
        ConfigCloudInfraEditCurrentView = new ConfigCloudInfraEditConfigView();

    }

    private void OnConfigInfraEdit(ConfigInfraEditEvent obj)
    {
        ConfigCloudInfraEditWindow = obj.ConfigCloudInfraEditWindow;
        IsOneActive = true;
        ConfigCloudInfraEditWindow.DataContext = obj.ServiceElementViewModel;
        ConfigCloudInfraEditWindow.Show();
    }
    
    private void OnConfigInfraEditCancel(ConfigInfraEditCancelEvent obj)
    {
        if(!obj.CancelEvent) return;
        IsOneActive = false;
        ConfigCloudInfraEditWindow.Close();
    }
}