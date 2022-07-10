namespace src.Domain.Event.@Shared;

public interface IEventDispatcher
{
    void Notify(IEvent @event);
    void Register(string eventName, IEventHandler<IEvent> eventHandler);
    void Unregister(string eventName);
    void UnregisterAll();
}