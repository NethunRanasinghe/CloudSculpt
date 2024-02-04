using CloudSculpt.Views.Windows;

namespace CloudSculpt.Commands;

public class ServiceElementCanvasEditCommand : CommandBase
{
    public override void Execute(object? parameter)
    {
        ConfigCloudInfraEditWindow configCloudInfraEditWindow = new ConfigCloudInfraEditWindow();
        configCloudInfraEditWindow.Show();
    }
}