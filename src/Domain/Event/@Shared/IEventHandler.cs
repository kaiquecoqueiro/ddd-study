namespace src.Domain.Event.@Shared;

public interface IEventHandler<in TEvent> where TEvent : IEvent
{
    void Handle(TEvent @event);
}
