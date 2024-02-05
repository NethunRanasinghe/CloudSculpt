using CloudSculpt.Views.Windows;

namespace CloudSculpt.Events;

public class ConfigInfraEditEvent
{
    public ConfigCloudInfraEditWindow ConfigCloudInfraEditWindow { get; }
    
    public ConfigInfraEditEvent(ConfigCloudInfraEditWindow configCloudInfraEditWindow)
    {
        ConfigCloudInfraEditWindow = configCloudInfraEditWindow;
    }
}