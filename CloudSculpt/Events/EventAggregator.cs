using System;
using System.Collections.Generic;

namespace CloudSculpt.Events;

public class EventAggregator
{
    public static EventAggregator Instance { get; } = new EventAggregator();

    private readonly Dictionary<Type, List<Action<object>>> _subscribers = new Dictionary<Type, List<Action<object>>>();

    public void Subscribe<TEvent>(Action<TEvent> subscriber)
    {
        var eventType = typeof(TEvent);

        if (!_subscribers.ContainsKey(eventType))
        {
            _subscribers[eventType] = new List<Action<object>>();
        }

        _subscribers[eventType].Add(obj => subscriber((TEvent)obj));
    }

    public void Publish<TEvent>(TEvent ev)
    {
        var eventType = typeof(TEvent);

        if (_subscribers.ContainsKey(eventType))
        {
            foreach (var subscriber in _subscribers[eventType])
            {
                subscriber(ev);
            }
        }
    }
}