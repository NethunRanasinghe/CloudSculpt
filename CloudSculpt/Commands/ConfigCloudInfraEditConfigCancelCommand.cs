using CloudSculpt.Events;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraEditConfigCancelCommand : CommandBase
{
    public override void Execute(object? parameter)
    {
        EventAggregator.Instance.Publish(new ConfigInfraEditCancelEvent(true));
    }
}