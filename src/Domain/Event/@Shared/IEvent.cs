using System;

namespace src.Domain.Event.@Shared;

public interface IEvent
{
    public DateTime DateTimeOcurred { get; }
}