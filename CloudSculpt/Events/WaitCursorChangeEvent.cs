namespace CloudSculpt.Events;

public class WaitCursorChangeEvent
{
    public bool IsWait { get; }
    
    public WaitCursorChangeEvent(bool isWait)
    {
        IsWait = isWait;
    }
}