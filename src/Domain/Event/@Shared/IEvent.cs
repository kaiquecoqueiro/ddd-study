using System;

namespace src.Domain.Event.@Shared;

public interface IEvent
{
    public object EventData { get; }
    public DateTime DateTimeOcurred { get; }
}