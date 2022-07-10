using System;
using src.Domain.Event.Shared;

namespace DDD.Study.Test.Mocks;

public class MockEvent : IEvent
{
    public MockEvent(string eventData, DateTime dateTimeOcurred)
    {
        EventData = eventData;
        DateTimeOcurred = dateTimeOcurred;
    }

    public string EventData { get; }
    public DateTime DateTimeOcurred { get; }
}

public class MockEventHandler : IEventHandler<IEvent>
{
    public void Handle(IEvent @event)
    {
        NumberOfTimeHandleWasCalled++;
    }

    public int NumberOfTimeHandleWasCalled { get; private set; }
}
