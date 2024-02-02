using CloudSculpt.ViewModels;

namespace CloudSculpt.Events;

public class RemoveServiceElementEvent
{
    public int ElementIndex { get; }
    
    public RemoveServiceElementEvent(int elementIndex)
    {
        ElementIndex = elementIndex;
    }
}