using CloudSculpt.ViewModels;
using CloudSculpt.Views.Windows;

namespace CloudSculpt.Events;

public class ConfigInfraEditEvent
{
    public ConfigCloudInfraEditWindow ConfigCloudInfraEditWindow { get; }
    public ServiceElementViewModel ServiceElementViewModel { get; }
    
    public ConfigInfraEditEvent(ConfigCloudInfraEditWindow configCloudInfraEditWindow, ServiceElementViewModel serviceElementViewModel)
    {
        ConfigCloudInfraEditWindow = configCloudInfraEditWindow;
        ServiceElementViewModel = serviceElementViewModel;
    }
}