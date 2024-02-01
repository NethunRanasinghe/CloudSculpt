using CloudSculpt.ViewModels;

namespace CloudSculpt.Events;

public class AddServiceElementEvent
{
    public ServiceElementViewModel ServiceElement { get; }

    public AddServiceElementEvent(ServiceElementViewModel serviceElement)
    {
        ServiceElement = serviceElement;
    }
}