namespace CloudSculpt.Events;

public class ConfigInfraEditCancelEvent
{
    public bool CancelEvent { get; }
    
    public ConfigInfraEditCancelEvent(bool cancelEvent)
    {
        CancelEvent = cancelEvent;
    }
}