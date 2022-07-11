using System;
using src.Domain.Event.Shared;

namespace src.Domain.Event.Customer
{
    public class CustomerCreatedEvent : IEvent
    {
        public DateTime DateTimeOcurred => DateTime.Now;
    }
}