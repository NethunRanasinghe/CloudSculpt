using System.Windows.Input;
using CloudSculpt.Commands;
using CloudSculpt.Events;
using CloudSculpt.Views.Windows;

namespace CloudSculpt.ViewModels;

public class ConfigCloudInfraEditViewModel : ViewModelBase
{
    private ConfigCloudInfraEditWindow _configCloudInfraEditWindow;

    public static bool IsOneActive { get; private set; }

    public ConfigCloudInfraEditWindow ConfigCloudInfraEditWindow
    {
        get => _configCloudInfraEditWindow;
        private set => SetField(ref _configCloudInfraEditWindow, value);
    }
    
    public ICommand ConfigCloudInfraEditWindowPointerPressedCommand { get; }

    public ConfigCloudInfraEditViewModel()
    {
        IsOneActive = false;
        
        // Commands
        ConfigCloudInfraEditWindowPointerPressedCommand = new ConfigCloudInfraEditWindowPointerPressedCommand(this);
        
        // Events
        EventAggregator.Instance.Subscribe<ConfigInfraEditEvent>(OnConfigInfraEdit);

    }

    private void OnConfigInfraEdit(ConfigInfraEditEvent obj)
    {
        ConfigCloudInfraEditWindow = obj.ConfigCloudInfraEditWindow;
        IsOneActive = true;
        ConfigCloudInfraEditWindow.Show();
    }
}