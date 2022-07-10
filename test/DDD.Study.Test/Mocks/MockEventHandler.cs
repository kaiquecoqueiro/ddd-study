using System;
using src.Domain.Event.Shared;

namespace DDD.Study.Test.Mocks;

public class MockEvent : IEvent
{
    public MockEvent(object eventData, DateTime dateTimeOcurred)
    {
        EventData = eventData;
        DateTimeOcurred = dateTimeOcurred;
    }

    public object EventData { get; }
    public DateTime DateTimeOcurred { get; }
}

public class MockEventHandler : IEventHandler<IEvent>
{
    public void Handle(IEvent @event)
    {
        throw new NotImplementedException();
    }
}
