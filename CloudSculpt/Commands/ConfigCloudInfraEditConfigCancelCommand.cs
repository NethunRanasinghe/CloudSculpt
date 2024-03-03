using CloudSculpt.Events;
using CloudSculpt.ViewModels;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraEditConfigCancelCommand(ServiceElementViewModel serviceElementViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        serviceElementViewModel.IsEditOpen = false;
        EventAggregator.Instance.Publish(new ConfigInfraEditCancelEvent(true));
    }
}