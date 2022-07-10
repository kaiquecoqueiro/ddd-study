using System.Collections.Generic;

namespace src.Domain.Event.@Shared;

public interface IEventDispatcher
{
    Dictionary<string, IList<IEventHandler<IEvent>>> GetEventHandlers();
    void Notify(IEvent @event);
    void Register(string eventName, IEventHandler<IEvent> eventHandler);
    void Unregister(string eventName);
    void UnregisterAll();
}